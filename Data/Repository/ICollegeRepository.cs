using System.Linq.Expressions;

namespace NewDotnetProject.Data.Repository
{
    public interface ICollegeRepository <T>{
    Task<List<T>> getAllAsync(Expression<Func<T , bool>> filter = null);
        Task<T> getStudentByAsync(Expression<Func<T , bool>> filter, bool useNoTracking = false);
        // Task<T> getStudentByNameAsync(Expression<Func<T , bool>> filter); 
        Task<T> createStudentAsync(T dbRecord);
        Task<T> updateStudentAsync(T dbRecord);
        Task<bool> deleteStudent(T dbRecord);
    }
}