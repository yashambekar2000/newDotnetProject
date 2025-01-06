


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewDotnetProject.Data.Repository;
using NewDotnetProject.Models;
using DataAlias = NewDotnetProject.Data;
using ModelAlias = NewDotnetProject.Models;

namespace newDotnetProject.Controllers
{
    [Route("api/[controller]")]
 [ApiController]
    public class AuthController : ControllerBase
  {

    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    // private readonly  ICollegeRepository<Student> _studentRepo;
    private readonly  IUserRepository _userRepo;

    private readonly IConfiguration _configuration;

    public AuthController(ILogger<AuthController> logger, IMapper mapper,IUserRepository userRepo, IConfiguration configuration)
    {
      _logger = logger;
      _mapper = mapper;
      _userRepo = userRepo;
      _configuration = configuration;
    }


    [HttpPost("create-user", Name = "createUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ModelAlias.UserDTO>> createUser([FromBody] ModelAlias.UserDTO model)
        {
        if (model == null) return BadRequest();
        var user = _mapper.Map<DataAlias.User>(model);
        var getRes = await _userRepo.createStudentAsync(user);
        model.Id = getRes.Id;
        return Ok(model);

        }


    [HttpPost("login", Name = "checkLogin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelAlias.LoginResponseDTO>> CheckLogin([FromBody] ModelAlias.LoginDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) {
                return BadRequest("Email or password cannot be null or empty.");
            }
                // var user = _mapper.Map<DataAlias.User>(model);
                var getRes = await _userRepo.checkUser(model.Email, model.Password);

                if (getRes == null) {
                    return NotFound("User not found.");
                }

                var userDto = _mapper.Map<ModelAlias.UserDTO>(getRes);
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor(){
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                         new Claim(ClaimTypes.Email , model.Email),
                         new Claim(ClaimTypes.Role ,  "Admin")
                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                  
                };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                 var token1 =  tokenHandler.WriteToken(token);
                  var loginResponseDTO = new LoginResponseDTO{
                    user = userDto,
                    token = token1
                  };
                return Ok(loginResponseDTO);
            
    }

    }
}