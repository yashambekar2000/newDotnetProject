using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDotnetProject.Data
{
    public class Item{
          [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id {get; set;}
        public string name {get; set;}
        public string description {get; set;}
        public double new_price {get; set;}
        public double old_price {get; set;}

         public string gender {get; set;}
        public string category {get; set;}
        public string image_url {get; set;}
        public int stock {get; set;}
        public ICollection<SavedItem> SavedItems { get; set; }
    }
}