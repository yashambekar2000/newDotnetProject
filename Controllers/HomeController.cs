
using System.Security.Claims;
using Newtonsoft.Json; // JSON serialization साठी

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewDotnetProject.Data.Repository;
using NewDotnetProject.Data;
using modelAlies = NewDotnetProject.Models;
using dataAlies = NewDotnetProject.Data;

using Microsoft.AspNetCore.Authorization;

namespace NewDotnetProject.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles ="Admin")]
  public class HomeController : ControllerBase
  {
    // **************** Dependency Injection *******************
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly  IStudentsRepository _studentRepo;
    private readonly IItemRepository _iRepo;
    private readonly IUserRepository _iUserRepo;

  private readonly CollegeDBContext _dbContext;
      // **************** Constructor *******************
    public HomeController(ILogger<HomeController> logger, IMapper mapper,IItemRepository iRepo,IUserRepository iUserRepo, CollegeDBContext dbContext)
    {
      _dbContext = dbContext;
      _logger = logger;
      _mapper = mapper;
      _iRepo = iRepo;
      _iUserRepo = iUserRepo;
    }



  // ************************ Getting all Items ****************************
        [HttpGet("get-items" , Name ="getItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<modelAlies.ItemDTO>>> getItems(string gender = null){
          if(gender == null){
           var data = await _iRepo.getAllAsync();
             return Ok(data);
          }else{
           var data = await _iRepo.getAllAsync(Item => Item.gender == gender);
             return Ok(data);
            }
          
        }
  // =====================================================================
  // ************************ Getting Saved Items ****************************
        [HttpGet("get-saved-items/{userId:int}" , Name ="getSavedItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<modelAlies.ItemDTO>>> getSavedItems1(int userId){
            var data = await _iUserRepo.getSavedItems(userId);
            return Ok(data);
        }
  // =====================================================================
 // ************************ Delete Saved Items ****************************
        [HttpDelete("delete-saved-items/{id:int}" , Name ="deleteSavedItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [AllowAnonymous]
    public async Task<ActionResult<string>> deleteSavedItemById(int id){
      var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        var getItem = await _iUserRepo.DeleteSavedItems(userId,id);
          if (!getItem)
          {
            _logger.LogWarning("Item with id not found.");
            return NotFound($"Item of id {id} not found.");

          }
         return Ok("Item deleted successfully."); 
    }
  // =====================================================================
  // ************************ Save Item ****************************
        [HttpPost("save-items" , Name ="SaveItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<modelAlies.ItemDTO>>> SaveItems1([FromBody]modelAlies.SavedItem model){
            var data = await _iUserRepo.SaveItems(model.UserId, model.ItemId);
               if (data){
                var savedItems = await _iUserRepo.getSavedItems(model.UserId);
                return Ok(savedItems);
            }else{
                return BadRequest("Failed to save item.");
            }        
        }
  // =====================================================================
  // ************************ Getting Dashboard Images ****************

        [HttpGet("get-home-images" , Name ="getHomeImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<modelAlies.HomeImagesDTO>>> getHomeImages(){
            var data = await _dbContext.HomeImages.ToListAsync();
            return Ok(data);
        }
  // =====================================================================
    }  
}
