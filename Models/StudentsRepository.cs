namespace NewDotnetProject.Models
{
    public static class StudentsRepository{
        public static List<Student> Students { get; set;} = new List<Student>(){
        new Student{
            Id = 1,
            studentName = "Yash",
            studentEmail = "yashambekar86@gmail.com",
            studentAddress = "Kharadi"
        },
         new Student{
            Id = 2,
            studentName = "Raviraj",
            studentEmail = "raviraj@gmail.com",
            studentAddress = "WTC"
        },
         new Student{
            Id = 3,
            studentName = "Rajesh",
            studentEmail = "rajesh@gmail.com",
            studentAddress = "Kothrud"
        }
       };
    }
}