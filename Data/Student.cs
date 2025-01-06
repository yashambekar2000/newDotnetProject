using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDotnetProject.Data
{
   public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id {get; set;}
         public string studentName {get; set;}
          public string studentEmail {get; set;}
           public string studentAddress {get; set;}
             public DateTime DOB {get; set;}
          public int? CollegeId { get; set; }

        public virtual College? College { get; set; } 
     }
}