       using System.ComponentModel.DataAnnotations;

namespace NewDotnetProject.Models
{
    public class HomeImagesDTO{
         public int Id {get; set;}
         [Required]
         [StringLength(100)]
        public string name {get; set;}

         public string gender {get; set;}
        public string category {get; set;}
        public string image_url {get; set;}
    }
    }