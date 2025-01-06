using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAlias = NewDotnetProject.Data;
using ModelAlias = NewDotnetProject.Models;

namespace newDotnetProject.Controllers
{
    [Route("api/[controller]")]
 [ApiController]
    public class CollegeController : ControllerBase{
        private readonly ILogger<CollegeController> _logger;
            private readonly DataAlias.CollegeDBContext _context;


   public CollegeController(ILogger<CollegeController> logger, DataAlias.CollegeDBContext context){
      _logger = logger;
    _context = context;
   }
    
  
    //=============== HTTP Get Request for getting Data ==========================
     [HttpGet("getColleges")]
      [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DataAlias.College>>> getColleges(){
      _logger.LogInformation("getColleges method started....");
     
        // var colleges = await _context.Colleges
        //                               .Select(college => new ModelAlias.CollegeDTO()
        //                               {
        //                                   Id = college.Id,
        //                                   collegeName = college.collegeName,
        //                                   collegeAddress = college.collegeAddress
                                        
        //                               })
        //                               .ToListAsync();
        var colleges = await _context.Colleges.ToListAsync();

       _logger.LogInformation("getColleges method ended....");
       return Ok(colleges);
       
    }

    }
}