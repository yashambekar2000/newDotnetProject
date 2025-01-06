using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewDotnetProject.Data.Repository;
using NewDotnetProject.Data;
using DataAlias = NewDotnetProject.Data;

// using NewDotnetProject.Configurations;
// using NewDotnetProject.Data;
// using NewDotnetProject.Models;
using ModelAlias = NewDotnetProject.Models;


namespace NewDotnetProject.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {

    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    // private readonly  ICollegeRepository<Student> _studentRepo;
    private readonly  IStudentsRepository _studentRepo;


    public HomeController(ILogger<HomeController> logger, IMapper mapper,IStudentsRepository studentRepo)
    {
      _logger = logger;
      _mapper = mapper;
      _studentRepo = studentRepo;
    }


    //=============== HTTP Get Request for getting Data ==========================
    [HttpGet("getStudents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModelAlias.StudentDTO>>> getStudents()
    {
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

      #region Copy studentDTO to studentData object =====================
      // Fetch students from the database
      // var students = await _context.Students
      //                               .Select(st => new ModelAlias.StudentDTO()
      //                               {
      //                                   Id = st.Id,
      //                                   studentName = st.studentName,
      //                                   studentAddress = st.studentAddress,
      //                                   studentEmail = st.studentEmail
      //                               })
      //                               .ToListAsync();

      // ============ by ussing auto mapper we copy student data in student dto ==========
      var studentData = await _studentRepo.getAllAsync();
      var studentMapData = _mapper.Map<List<ModelAlias.StudentDTO>>(studentData);
      #endregion

      _logger.LogInformation("getStudents method ended....");
      return Ok(studentMapData);

    }

    //=============== HTTP Get Request for getting Data by id ==========================
    [HttpGet("getStudent/{id:int}", Name = "getStudentById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelAlias.StudentDTO>> getStudentById(int id)
    {
      //  return ModelAlias.StudentsRepository.Students.Where(n => n.Id == id).FirstOrDefault();
      var getStudent = await _studentRepo.getStudentByAsync(student => student.Id == id);
      if (getStudent == null) return NotFound();
      #region Copy studentDTO to studentData object ================
      // var st = new ModelAlias.StudentDTO{
      //    Id = getStudent.Id,
      //    studentName = getStudent.studentName,
      //    studentAddress = getStudent.studentAddress,
      //    studentEmail = getStudent.studentEmail
      // };

      var st = _mapper.Map<ModelAlias.StudentDTO>(getStudent);
      #endregion
      return Ok(st);
    }


    //=============== HTTP Get Request for getting Data by id ==========================
    [HttpGet("getStudentName/{name:alpha}", Name = "getStudentByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelAlias.StudentDTO>> getStudentByName(string name)
    {
      //  return ModelAlias.StudentsRepository.Students.Where(n => n.Id == id).FirstOrDefault();
      var getStudent = await _studentRepo.getStudentByAsync(student => student.studentName.ToLower().Contains(name.ToLower()));
      if (getStudent == null) return NotFound();
      var st = _mapper.Map<ModelAlias.StudentDTO>(getStudent);
      return Ok(st);
    }

    //=============== HTTP Delete Request for Removing Data ==========================
    [HttpDelete("deleteStudent/{id:int}", Name = "deleteStudentById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> deleteStudentById(int id)
    {
      //   var student = ModelAlias.StudentsRepository.Students.Where(n => n.Id == id).FirstOrDefault();
      // var _student = await _context.Students.FirstOrDefaultAsync(n => n.Id == id);
        var getStudent = await _studentRepo.getStudentByAsync(student => student.Id == id, true);

      if (getStudent == null)
      {
        _logger.LogWarning("Student with id not found.");
        return NotFound($"student of id {id} not found.");

      }
      //   ModelAlias.StudentsRepository.Students.Remove(student);
     bool getRes = await _studentRepo.deleteStudent(getStudent);
      // await _context.SaveChangesAsync();
      if(getRes) {
         return Ok("Student deleted successfully.");
      }else{
        return NotFound("Student not deleted successfully.");
      }
     
    }

    //=============== HTTP Post Request for Creating Data ==========================
    [HttpPost("createStudent", Name = "createStudentById")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModelAlias.StudentDTO>> createStudent([FromBody] ModelAlias.StudentDTO model)
    {
      if (model == null) return BadRequest();

      // if(!ModelState.IsValid) return BadRequest(ModelState);

      // if(model.AdmissionDate < DateTime.Now) {
      //    //1. directly add error message to modelState
      //    //2. using customer attribute
      //    ModelState.AddModelError("AdmissionDate error","AdmissionDate can be greater than or equal to todays date.");
      //    return BadRequest(ModelState);
      // }

      // int newId = ModelAlias.StudentsRepository.Students.LastOrDefault().Id + 1;
      // Map ModelAlias.StudentDTO to NewDotnetProject.Data.Student 

      #region Copy studentDTO to studentData object ==============
      //  var st = new NewDotnetProject.Data.Student{
      //      // Id = newId,
      //      studentName = model.studentName,
      //      studentAddress = model.studentAddress,
      //      studentEmail = model.studentEmail,
      //      DOB = DateTime.UtcNow
      //   };
      var st = _mapper.Map<DataAlias.Student>(model);
      #endregion

      // Add the mapped student record to the database
      // await _context.Students.AddAsync(st);
      // await _context.SaveChangesAsync();
      
      var getRes = await _studentRepo.createStudentAsync(st);
      // ModelAlias.StudentsRepository.Students.Add(st);
      model.Id = getRes.Id;
      // return Ok(model);

      //====== it returns the get students url with id 
      //========also returns model of specific id
      return CreatedAtRoute("getStudentById", new { id = model.Id }, model);

    }

    //=============== HTTP Put Request for Updating Data ==========================
    [HttpPut("updateStudent", Name = "updateStudentById")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModelAlias.StudentDTO>> updateStudent([FromBody] ModelAlias.StudentDTO model)
    {
      if (model == null || model.Id <= 0)
      {
        _logger.LogWarning("bad request in updateStudent method.");
        return BadRequest();
      }
      // var getStudent = ModelAlias.StudentsRepository.Students.Where(n => n.Id == model.Id).FirstOrDefault();
      // var getStudent = _context.Students.AsNoTracking().FirstOrDefault(n => n.Id == model.Id);
      var getStudent = await _studentRepo.getStudentByAsync(student => student.Id == model.Id,true);
      if (getStudent == null)
      {
        _logger.LogError("student with id not found in updateStudent method.");
       return NotFound();
      }
      #region  Copy studentDTO to studentData object ==============
      // getStudent.studentName = model.studentName;
      // getStudent.studentEmail = model.studentEmail;
      // getStudent.studentAddress = model.studentAddress;

      var newStudent = _mapper.Map<DataAlias.Student>(model);
      #endregion
    //  var getRes = await _studentRepo.updateStudentAsync(getStudent);
    await _studentRepo.updateStudentAsync(newStudent);
      // _context.Update(getStudent);
      // _context.SaveChanges();
      return Ok(model);
    }



    //=============== HTTP Patch Request for Updating Data ==========================

    [HttpPatch("{id:int}/updateStudentPartial", Name = "updateStudentByIdPartial")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> updateStudentPartial(int id, [FromBody] JsonPatchDocument<ModelAlias.StudentDTO> patchDocument)
    {
      if (patchDocument == null || id <= 0) BadRequest();
      // var getStudent = _context.Students.AsNoTracking().FirstOrDefault(n => n.Id == id);
      var getStudent = await _studentRepo.getStudentByAsync(student => student.Id == id , true);
      if (getStudent == null) return NotFound();
      // var stu = new ModelAlias.StudentDTO{
      //    Id = getStudent.Id,
      //    studentName = getStudent.studentName,
      //    studentEmail = getStudent.studentEmail,
      //    studentAddress = getStudent.studentAddress
      // };
      var stu = _mapper.Map<ModelAlias.StudentDTO>(getStudent);
      patchDocument.ApplyTo(stu, ModelState);

      if (!ModelState.IsValid) return BadRequest(ModelState);
      #region using mapper for copying dto data to data table ============
      // getStudent.studentName = stu.studentName;
      // getStudent.studentEmail = stu.studentEmail;
      // getStudent.studentAddress = stu.studentAddress;
      var newStudent = _mapper.Map<DataAlias.Student>(stu);

      //--------its important to use update method when using _mapper -----------------
      await _studentRepo.updateStudentAsync(newStudent);
      #endregion

      // _context.SaveChanges();
      return NoContent();
    }

    //=============== Api for Logging test ==========================
    [HttpGet("test/logging", Name = "testLogging")]
    public ActionResult testLogging()
    {

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
