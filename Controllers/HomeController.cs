
using System.Security.Claims;
using Newtonsoft.Json; // JSON serialization साठी
using System.Net;
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
    private  modelAlies.ApiResponse _apiResponse;

  private readonly CollegeDBContext _dbContext;
      // **************** Constructor *******************
    public HomeController(ILogger<HomeController> logger, IMapper mapper,IItemRepository iRepo,IUserRepository iUserRepo, CollegeDBContext dbContext)
    {
      _dbContext = dbContext;
      _logger = logger;
      _mapper = mapper;
      _iRepo = iRepo;
      _iUserRepo = iUserRepo;
      _apiResponse = new();
    }



  // ************************ Getting all Items ****************************
        [HttpGet("get-items" , Name ="getItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
  public async Task<ActionResult<modelAlies.ApiResponse>> getItems(string gender = null){
      try{
         if(gender == null){
          //  var data = await _iRepo.getAllAsync();
          _apiResponse.Data =  await _iRepo.getAllAsync();
          _apiResponse.Status = true;
          // _apiResponse.StatusCode = HttpStatusCode.Status200OK;
             return Ok(_apiResponse);
          }else{
          //  var data = await _iRepo.getAllAsync(Item => Item.gender == gender);
          //    return Ok(data);
           _apiResponse.Data =  await _iRepo.getAllAsync(Item => Item.gender == gender);
           _apiResponse.Status = true;
          //  _apiResponse.StatusCode = HttpStatusCode.Status200OK;
          return Ok(_apiResponse);
          }
      }catch(Exception ex){
            _apiResponse.Errors.Add(ex.Message);
            // _apiResponse.StatusCode = HttpStatusCode.Status500InternalServerError;
            _apiResponse.Status = false;
          return _apiResponse;
      }   
     }
  // =====================================================================
  // ************************ Getting Saved Items ****************************
        [HttpGet("get-saved-items/{userId:int}" , Name ="getSavedItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        // [AllowAnonymous]
   public async Task<ActionResult<modelAlies.ApiResponse>> getSavedItems1(int userId){
        try{
             _apiResponse.Data =  await _iUserRepo.getSavedItems(userId);
             _apiResponse.Status = true;
            //  _apiResponse.StatusCode = HttpStatusCode.Status200OK;
            return Ok(_apiResponse);
        }catch(Exception ex){
            _apiResponse.Errors.Add(ex.Message);
            // _apiResponse.StatusCode = HttpStatusCode.Status500InternalServerError;
            _apiResponse.Status = false;
          return _apiResponse;
       }   
          
    }
  // =====================================================================
 // ************************ Delete Saved Items ****************************
        [HttpDelete("delete-saved-items/{id:int}" , Name ="deleteSavedItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [AllowAnonymous]
  public async Task<ActionResult<modelAlies.ApiResponse>> deleteSavedItemById(int id){
     try{
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        var getItem = await _iUserRepo.DeleteSavedItems(userId,id);
          if (!getItem)
          {
             _logger.LogWarning("Item with id not found.");
             _apiResponse.Errors.Add($"Item of id {id} not found.") ;
             _apiResponse.Status = false;
            //  _apiResponse.StatusCode = HttpStatusCode.Status404NotFound;
          return _apiResponse;

          }
              _apiResponse.Data =  "Item deleted successfully.";
              _apiResponse.Status = true;
              // _apiResponse.StatusCode = HttpStatusCode.Status200OK;
             return Ok(_apiResponse);

        }catch(Exception ex){
            _apiResponse.Errors.Add(ex.Message);
            // _apiResponse.StatusCode = HttpStatusCode.Status500InternalServerError;
            _apiResponse.Status = false;
           return _apiResponse;
         }
    }
  // =====================================================================
  // ************************ Save Item ****************************
        [HttpPost("save-items" , Name ="SaveItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
      public async Task<ActionResult<modelAlies.ApiResponse>> SaveItems1([FromBody]modelAlies.SavedItem model){
         try{
            var data = await _iUserRepo.SaveItems(model.UserId, model.ItemId);
               if (data){
                _apiResponse.Data = await _iUserRepo.getSavedItems(model.UserId);
                _apiResponse.Status = true;
                // _apiResponse.StatusCode = HttpStatusCode.Status200OK;
                return Ok(_apiResponse);
            }else{
                _apiResponse.Errors.Add("Failed to save item.");
                // _apiResponse.StatusCode = HttpStatusCode.Status400BadRequest;
                _apiResponse.Status = false;
               return _apiResponse;
            }  
          }catch(Exception ex){
            _apiResponse.Errors.Add(ex.Message);
            // _apiResponse.StatusCode = HttpStatusCode.Status500InternalServerError;
            _apiResponse.Status = false;
            return _apiResponse;
          }      
        }
  // =====================================================================
  // ************************ Getting Dashboard Images ****************

        [HttpGet("get-home-images" , Name ="getHomeImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
         [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
      public async Task<ActionResult<modelAlies.ApiResponse>> getHomeImages(){
         try{
            _apiResponse.Data = await _dbContext.HomeImages.ToListAsync();
            _apiResponse.Status = true;
            // _apiResponse.StatusCode = HttpStatusCode.Status200OK;
            return Ok(_apiResponse);
         }catch(Exception ex){
            _apiResponse.Errors.Add(ex.Message);
            // _apiResponse.StatusCode = HttpStatusCode.Status500InternalServerError;
            _apiResponse.Status = false;
          return _apiResponse;
          }
        }
  // =====================================================================
    }  
}
