namespace NewDotnetProject.Data.Repository
{
    public class ItemRepoClass : CollegeRepoClass<Item> , IItemRepository{

    private readonly CollegeDBContext _DBContext;
        public ItemRepoClass(CollegeDBContext DBContext) : base(DBContext){
        _DBContext = DBContext;
        }
    }
}