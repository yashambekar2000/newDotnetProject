using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
// using NewDotnetProject.Data;
using DataAlias = NewDotnetProject.Data;


namespace NewDotnetProject.Data.Repository
{
    public class CollegeRepoClass<T> : ICollegeRepository<T> where T : class{

    private readonly DataAlias.CollegeDBContext _dbContext;
    private DbSet<T> _dbSet;
        public CollegeRepoClass( DataAlias.CollegeDBContext dbContext){
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
         public async Task<List<T>>  getAllAsync(Expression<Func<T , bool>> filter = null){
            if (filter != null)
            {
                return await _dbSet.Where(filter).ToListAsync();
            }
            return await _dbSet.ToListAsync();
            //  return await _dbSet.where;
         }
        public async Task<T>  getStudentByAsync(Expression<Func<T , bool>> filter /* --> predicates and Delegatges */, bool useNoTracking = false){
            if(useNoTracking)
             return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);
             else
                return await _dbSet.FirstOrDefaultAsync(filter);
         }

        // public async Task<T>  getStudentByNameAsync(Expression<Func<T , bool>> filter){
        //     return await _dbSet.Where(filter).FirstOrDefaultAsync();
        //  }

        public async Task<T> createStudentAsync(T dbRecord){
           _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
         }

     
        public async Task<T>  updateStudentAsync(T dbRecord){

             _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
         }


        public async Task<bool>  deleteStudent(T dbRecord){
             _dbSet.Remove(dbRecord);
             await _dbContext.SaveChangesAsync();
              return true;
         }

    }
}