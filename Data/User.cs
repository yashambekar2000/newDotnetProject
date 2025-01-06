using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDotnetProject.Data
{
   public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id {get; set;}
         public string name {get; set;}
          public string email {get; set;}
           public string password {get; set;}
         public ICollection<SavedItem> SavedItems { get; set; }
     }
}