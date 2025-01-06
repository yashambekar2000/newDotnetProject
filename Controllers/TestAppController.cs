using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewDotnetProject.Data.Repository;
using modelAlies = NewDotnetProject.Models;
using dataAlies = NewDotnetProject.Data;
using Microsoft.AspNetCore.Authorization;

namespace NewDotnetProject.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles ="Admin")]
    public class TestAppController : ControllerBase{

        private readonly ILogger<TestAppController> _iLogger;
        private readonly IMapper _mapper;
        private readonly IItemRepository _iRepo;
         private readonly IUserRepository _iUserRepo;

        public TestAppController(IMapper mapper, ILogger<TestAppController> iLogger, IItemRepository iRepo,IUserRepository iUserRepo){
            _iLogger = iLogger;
            _iRepo = iRepo;
             _iUserRepo = iUserRepo;
            _mapper = mapper;
        }

        [HttpGet("get-items" , Name ="getItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<modelAlies.ItemDTO>>> getItems(){
            var data = await _iRepo.getAllAsync();
            return Ok(data);
        }

         [HttpGet("get-saved-items/{userId:int}" , Name ="getSavedItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<modelAlies.ItemDTO>>> getSavedItems1(int userId){
            var data = await _iUserRepo.getSavedItems(userId);
            return Ok(data);
        }

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

    }
}