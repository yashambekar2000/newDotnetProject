

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDotnetProject.Data
{
   public class College
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id {get; set;}
         public string collegeName {get; set;}
           public string collegeAddress {get; set;}
    
    }
}