using System.ComponentModel.DataAnnotations;
using NewDotnetProject.Validators;

namespace NewDotnetProject.Models
{
    public class UserDTO{ 
         public int Id {get; set;}

         [StringLength(100)]
         public string name {get; set;}

          [EmailAddress]
          public string email {get; set;}

           public string password {get; set;}


    }
}