using System.ComponentModel.DataAnnotations;

namespace NewDotnetProject.Models
{
    public class ItemDTO{
         public int Id {get; set;}
         [Required]
         [StringLength(100)]
        public string name {get; set;}
        public string description {get; set;}
         [Required]
        public double new_price {get; set;}
         [Required]
        public double old_price {get; set;}

         public string gender {get; set;}
          [Required]
        public string category {get; set;}
         [Required]
        public string image_url {get; set;}
        public int stock {get; set;}
    }
}