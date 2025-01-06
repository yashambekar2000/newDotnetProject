// using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Repository
{
    public interface IStudentsRepository : ICollegeRepository<Student>{
        // Task<List<Student>> getAllAsync();
        // Task<Student> getStudentByIdAsync(int id, bool useNoTracking = false);
        // Task<Student> getStudentByNameAsync(string name); 
        // Task<int> createStudentAsync(Student student);
        // Task<int> updateStudentAsync(Student student);
        // Task<bool> deleteStudent(Student student);
        Task<List<Student>> getStudentByFeeStatus(int feeStatus);
    }
}