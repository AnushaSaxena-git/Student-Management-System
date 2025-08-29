using AutoMapper;
using Crudbyme.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Crudbyme.Models
{
    public class Departmentrepo : IDepartment {

        
        
            private readonly studentdbcontext _context; // Correct name for DbContext
            private readonly IMapper _mapper;

            // Constructor
            public Departmentrepo(studentdbcontext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); // Saves all changes made to the DbContext
        }
        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            // Retrieve the department from the database by its ID
            var department = await _context.Departments
                .FirstOrDefaultAsync(s => s.DeptId == id);

            // If no department is found, return null or handle the case accordingly
            if (department == null)
            {
                return null; // or handle differently if you need a different return type
            }

            // Map the department to DepartmentDTO using AutoMapper
            return _mapper.Map<DepartmentDTO>(department);
        }

        //public async Task<List<DepartmentDTO>> GetStudentsBySP(string query)
        //{
        //    // Map students to DTOs using AutoMapper
        //    var departments = _context.Departments
        //           .FromSqlRaw("EXEC dbo.FilterDepartmentByName @NamePart",
        //                       new SqlParameter("@NamePart", query ?? (object)DBNull.Value))
        //           .ToList();
        //    var departmentDtos = _mapper.Map<List<DepartmentDTO>>(departments);

        //    return departmentDtos;
        //}

        public async Task<List<DepartmentDTO>> GetDepartment()
        {
            // Fetch all students asynchronously
            var Departments = await _context.Departments.ToListAsync();
            // Map students to DTOs using AutoMapper
            var DepartmentDtos = _mapper.Map<List<DepartmentDTO>>(Departments);
            return DepartmentDtos; // Return the list of DTOs
        }
    }
}
