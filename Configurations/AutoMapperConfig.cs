using AutoMapper;
using DataAlises = NewDotnetProject.Data;
using ModalAlises = NewDotnetProject.Models;

namespace NewDotnetProject.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig(){
            // CreateMap<DataAlises.Student,ModalAlises.StudentDTO>().ForMember(n => n.student_name , opt => opt.MapFrom(x => x.studentName)).ReverseMap();
                //    CreateMap<DataAlises.Student,ModalAlises.StudentDTO>().ForMember(n => n.studentName , opt => opt.Ignore()).ReverseMap();

            //  CreateMap<DataAlises.Student,ModalAlises.StudentDTO>().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Address Found" : n).ReverseMap();
             CreateMap<DataAlises.Student,ModalAlises.StudentDTO>().ForMember(n => n.studentAddress , opt => opt.MapFrom(n =>  string.IsNullOrEmpty(n.studentAddress) ? "No Address Found" : n.studentAddress)).ReverseMap();
            CreateMap<DataAlises.User,ModalAlises.UserDTO>().ReverseMap();
          CreateMap<DataAlises.Item,ModalAlises.ItemDTO>().ReverseMap();


        }
    }
}