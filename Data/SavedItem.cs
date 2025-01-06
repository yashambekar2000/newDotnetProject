namespace NewDotnetProject.Data
{
    public class SavedItem
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; }
}
}