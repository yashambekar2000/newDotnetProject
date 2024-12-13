using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAlias =  NewDotnetProject.Data;
using ModelAlias = NewDotnetProject.Models;


namespace NewDotnetProject.Controllers
{
   
[Route("api/[controller]")]
 [ApiController]
  public class HomeController : ControllerBase{

   private readonly ILogger<HomeController> _logger;
    private readonly DataAlias.CollegeDBContext _context;


   public HomeController(ILogger<HomeController> logger, DataAlias.CollegeDBContext context){
      _logger = logger;
    _context = context;
   }
    
  
    //=============== HTTP Get Request for getting Data ==========================
     [HttpGet("getStudents")]
      [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModelAlias.StudentDTO>>> getStudents(){
      _logger.LogInformation("getStudents method started....");
      // var students = new List<StudentDTO>();
      // foreach(var item in StudentsRepository.Students){
      //    StudentDTO obj = new StudentDTO()
      //    {
      //       Id = item.Id,
      //       studentName = item.studentName,
      //       studentAddress = item.studentAddress,
      //       studentEmail = item.studentEmail
      //    };
      //    students.Add(obj);
      // }
      // var students = ModelAlias.StudentsRepository.Students.Select(st => new ModelAlias.StudentDTO(){
      //    Id = st.Id,
      //    studentName = st.studentName,
      //    studentAddress = st.studentAddress,
      //    studentEmail  = st.studentEmail
      // });

        // Fetch students from the database
        var students = await _context.Students
                                      .Select(st => new ModelAlias.StudentDTO()
                                      {
                                          Id = st.Id,
                                          studentName = st.studentName,
                                          studentAddress = st.studentAddress,
                                          studentEmail = st.studentEmail
                                      })
                                      .ToListAsync();

       _logger.LogInformation("getStudents method ended....");
       return Ok(students);
       
    }

    //=============== HTTP Get Request for getting Data by id ==========================
     [HttpGet("getStudent/{id:int}", Name = "getStudentById" )]
      [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ModelAlias.Student> getStudentById(int id){
      //  return ModelAlias.StudentsRepository.Students.Where(n => n.Id == id).FirstOrDefault();
      var getStudent =  _context.Students.FirstOrDefault(n => n.Id == id);
      if(getStudent == null) return NotFound();
      var st = new ModelAlias.StudentDTO{
         Id = getStudent.Id,
         studentName = getStudent.studentName,
         studentAddress = getStudent.studentAddress,
         studentEmail = getStudent.studentEmail
      };
       return Ok(st);
    }

    //=============== HTTP Delete Request for Removing Data ==========================
     [HttpDelete("deleteStudent/{id:int}", Name = "deleteStudentById" )]
      [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> deleteStudentById(int id){
      //   var student = ModelAlias.StudentsRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        var _student = _context.Students.FirstOrDefault(n => n.Id == id);
            //   var getStudent = _context.Students.FirstOrDefault(n => n.Id == model.Id);

        if(_student == null){
          _logger.LogWarning("Student with id not found.");
            return NotFound($"student of id {id} not found.");
            
        }
         //   ModelAlias.StudentsRepository.Students.Remove(student);
         _context.Remove(_student);
         _context.SaveChanges();
        return Ok("Student deleted successfully.");
    }

    //=============== HTTP Post Request for Creating Data ==========================
    [HttpPost("createStudent", Name = "createStudentById" )]
      [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModelAlias.StudentDTO>> createStudent([FromBody] ModelAlias.StudentDTO model){
      if(model == null) return BadRequest();

      // if(!ModelState.IsValid) return BadRequest(ModelState);

      // if(model.AdmissionDate < DateTime.Now) {
      //    //1. directly add error message to modelState
      //    //2. using customer attribute
      //    ModelState.AddModelError("AdmissionDate error","AdmissionDate can be greater than or equal to todays date.");
      //    return BadRequest(ModelState);
      // }

      // int newId = ModelAlias.StudentsRepository.Students.LastOrDefault().Id + 1;
       // Map ModelAlias.StudentDTO to NewDotnetProject.Data.Student 
     var st = new NewDotnetProject.Data.Student{
         // Id = newId,
         studentName = model.studentName,
         studentAddress = model.studentAddress,
         studentEmail = model.studentEmail,
         DOB = DateTime.UtcNow
      };
   

    // Add the mapped student record to the database
    _context.Students.Add(st);
    await _context.SaveChangesAsync();
      
      // ModelAlias.StudentsRepository.Students.Add(st);
      model.Id = st.Id ;
      // return Ok(model);

      //====== it returns the get students url with id 
      //========also returns model of specific id
      return CreatedAtRoute("getStudentById", new {id = model.Id },model);

    }

    //=============== HTTP Put Request for Updating Data ==========================
    [HttpPut("updateStudent", Name ="updateStudentById")]
 [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult updateStudent([FromBody] ModelAlias.StudentDTO model){
      if(model == null || model.Id <=0)
      {
         _logger.LogWarning("bad request in updateStudent method.");
          BadRequest();
      }
      // var getStudent = ModelAlias.StudentsRepository.Students.Where(n => n.Id == model.Id).FirstOrDefault();
      var getStudent = _context.Students.FirstOrDefault(n => n.Id == model.Id);
      if(getStudent == null )
     {
       _logger.LogError("student with id not found in updateStudent method.");
         NotFound();
     }
      getStudent.studentName = model.studentName;
      getStudent.studentEmail = model.studentEmail;
      getStudent.studentAddress = model.studentAddress;

      _context.SaveChanges();
      return NoContent();
    }



    //=============== HTTP Patch Request for Updating Data ==========================

     [HttpPatch("{id:int}/updateStudentPartial", Name ="updateStudentByIdPartial")]
 [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public ActionResult updateStudentPartial(int id , [FromBody] JsonPatchDocument<ModelAlias.StudentDTO> patchDocument){
      if(patchDocument == null || id <=0) BadRequest();
      var getStudent = _context.Students.FirstOrDefault(n => n.Id == id);
      if(getStudent == null ) NotFound();
      var stu = new ModelAlias.StudentDTO{
         Id = getStudent.Id,
         studentName = getStudent.studentName,
         studentEmail = getStudent.studentEmail,
         studentAddress = getStudent.studentAddress
      };
      patchDocument.ApplyTo(stu,ModelState);

      if(!ModelState.IsValid) BadRequest(ModelState);
      getStudent.studentName = stu.studentName;
      getStudent.studentEmail = stu.studentEmail;
      getStudent.studentAddress = stu.studentAddress;

      _context.SaveChanges();
      return NoContent();
    }

     //=============== Api for Logging test ==========================
     [HttpGet("test/logging", Name = "testLogging" )]
    public ActionResult testLogging(){

             _logger.LogTrace("log from trace method");
             _logger.LogDebug("log from debug method");
             _logger.LogInformation("log from information method");
             _logger.LogWarning("log from warning method");
             _logger.LogError("log from error method");
             _logger.LogCritical("log from critical method");

      return Ok();
    }


}  
}
