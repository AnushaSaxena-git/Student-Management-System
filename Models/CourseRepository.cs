using AutoMapper;
using Crudbyme.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Crudbyme.Models
{
    public class CourseRepository : ICourse
    {
        //public class Departmentrepo : IDepartment




        private readonly studentdbcontext _context; 
        private readonly IMapper _mapper;

        // Constructor
        public CourseRepository(studentdbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       public IEnumerable<Course> GetCoursesByDepartment(int departmentId)
        {


            return _context.Courses
                       .Where(c => c.DeptId == departmentId) // Filter by department ID
                       .ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); // Saves all changes made to the DbContext
        }
        public async Task<CourseDTO> GetByIdAsync(int id)
        {

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);

            // If no student is found, return null or handle the case accordingly
            return course == null ? null : _mapper.Map<CourseDTO>(course); // Using AutoMapper
        }

        public async Task<List<CourseDTO>> GetCourse()
        {
            // Fetch all students asynchronously
            var Courses = await _context.Courses.ToListAsync();
            // Map students to DTOs using AutoMapper
            var CourseDtos = _mapper.Map<List<CourseDTO>>(Courses);
            return CourseDtos; // Return the list of DTOs
        }
    }
}
