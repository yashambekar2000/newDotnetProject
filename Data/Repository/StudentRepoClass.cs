using Microsoft.EntityFrameworkCore;
// using NewDotnetProject.Data;
using DataAlias = NewDotnetProject.Data;


namespace NewDotnetProject.Data.Repository
{
    public class StudentRepoClass : CollegeRepoClass<Student>, IStudentsRepository{

    private readonly DataAlias.CollegeDBContext _dbContext;
        public StudentRepoClass( DataAlias.CollegeDBContext dbContext) : base(dbContext){
            _dbContext = dbContext;
        }
        //  public async Task<List<Student>>  getAllAsync(){
        //     return await _dbContext.Students.ToListAsync();
        //  }
        // public async Task<Student>  getStudentByIdAsync(int id, bool useNoTracking = false){
        //     if(useNoTracking)
        //      return await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        //      else
        //         return await _dbContext.Students.FirstOrDefaultAsync(n => n.Id == id);
        //  }

        // public async Task<Student>  getStudentByNameAsync(string name){
        //     return await _dbContext.Students.Where(n => n.studentName.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
        //  }

        // public async Task<int> createStudentAsync(Student student){
        //    _dbContext.Students.Add(student);
        //     await _dbContext.SaveChangesAsync();
        //     return student.Id;
        //  }

     
    //     public async Task<int>  updateStudentAsync(Student student){
    //          var getStudent = await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(n => n.Id == student.Id);
    //   if (getStudent == null) throw new ArgumentNullException($"No Student found with id: {student.Id}");
      
    //         getStudent.studentName = student.studentName;
    //         getStudent.studentEmail = student.studentEmail;
    //         getStudent.studentAddress = student.studentAddress;

    //          _dbContext.Update(getStudent);
    //         await _dbContext.SaveChangesAsync();
    //         return student.Id;
    //      }


        // public async Task<bool>  deleteStudent(Student student){
        //      _dbContext.Students.Remove(student);
        //      await _dbContext.SaveChangesAsync();
        //       return true;
        //  }


          public async Task<List<Student>>  getStudentByFeeStatus(int feeStatus){
            
              return null;
         }

    }
}