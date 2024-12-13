using System.ComponentModel.DataAnnotations;
using NewDotnetProject.Validators;

namespace NewDotnetProject.Models
{
    public class StudentDTO{ 
         public int Id {get; set;}

         [Required(ErrorMessage ="Student name required .....")]
         [StringLength(100)]
         public string studentName {get; set;}

          [EmailAddress]
          public string studentEmail {get; set;}

           [Required]
           public string studentAddress {get; set;}

            [Required]
            [DateCheck]
           public DateTime AdmissionDate {get; set;}

    }
}