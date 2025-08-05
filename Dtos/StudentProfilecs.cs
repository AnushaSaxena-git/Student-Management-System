namespace Crudbyme.Dtos
{
    using AutoMapper;
    using global::Crudbyme.Models;

    namespace Crudbyme.Models
    {
        public class StudentProfile : Profile
        {
            public StudentProfile()
            {
                CreateMap<student, StudentDto>();
                CreateMap<StudentDto, student>();
                CreateMap<CourseDTO, Course>();
                CreateMap<Course, CourseDTO>().ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DeptName)); // Assuming `DepartmentName` is a property of the `Department` class
                ;
                CreateMap<Department, DepartmentDTO>();
                CreateMap<DepartmentDTO, Department>();
            }
        }
    }

}
