using System.ComponentModel.DataAnnotations;
using NewDotnetProject.Validators;

namespace NewDotnetProject.Models
{
    public class CollegeDTO{ 
         public int Id {get; set;}

         [Required(ErrorMessage ="Student name required .....")]
         [StringLength(100)]
         public string collegeName {get; set;}

           [Required]
           public string collegeAddress {get; set;}

    }
}