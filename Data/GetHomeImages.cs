using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDotnetProject.Data
{
    public class GetHomeImages{
          [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id {get; set;}
        public string name {get; set;}

         public string gender {get; set;}
        public string category {get; set;}
        public string image_url {get; set;}
    
    }
}