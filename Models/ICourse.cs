using Crudbyme.Dtos;

namespace Crudbyme.Models
{
    public interface ICourse
    {
        Task<CourseDTO> GetByIdAsync(int id);
        Task SaveChangesAsync();
        IEnumerable<Course> GetCoursesByDepartment(int departmentId);


        Task<List<CourseDTO>> GetCourse();
    }
}
