using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NewDotnetProject.Models
{
    public class ApiResponse{ 
         public bool Status {get; set;}
         public HttpStatusCode StatusCode {get; set;}
        public dynamic  Data {get; set;}
         public List<string>  Errors {get; set;}

    }
}