using Crudbyme;
using Crudbyme.Models;
using AutoMapper;
using Crudbyme.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Crudbyme.Models
{
    public class StudentRepository : IStudentRepository
    {
        private readonly studentdbcontext _context; 
        private readonly IMapper _mapper;

        // Constructor
        public StudentRepository(studentdbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Add a new student
        public async Task AddAsync(StudentDto studentDto)
        {
            //  Find the department by name
            var department = await _context.Departments
                                            .FirstOrDefaultAsync(d => d.DeptId == studentDto.DeptId);


           // var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == studentDto.CourseId);


            //  Mapping DTO to entity student
            var student = _mapper.Map<student>(studentDto);

            //student.StudentId = course.CourseId;

            //  Set the DeptId for the student entity
            student.DeptId = department.DeptId;

            //  Add the student to the context and mark it as added
           _context.Students.Add(student);

            //  Save changes to the database
            await _context.SaveChangesAsync();
        }


        // Delete a student by ID
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        // Get all students as DTOs
        public async Task<List<StudentDto>> GetStudents()
        {
            // Fetch all students asynchronously
            var students = await _context.Students.ToListAsync();
            // Map students to DTOs using AutoMapper
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return studentDtos; // Return the list of DTOs
        }

        public async Task<List<StudentDto>> GetStudentsBySP(string query)
        {
            // Map students to DTOs using AutoMapper
            var students = _context.Students
                   .FromSqlRaw("EXEC dbo.FilterStudentsByName @NamePart",
                               new SqlParameter("@NamePart", query ?? (object)DBNull.Value))
                   .ToList();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return studentDtos;
        }

        public IQueryable<student> GetQuery()
        {
            // Fetch all students asynchronously
            return _context.Students.AsQueryable();
        }

        // Get student by ID as a DTO

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DeptId == id);

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);


            // Log the result for debugging
            if (student == null)
            {
                Console.WriteLine($"No student found with ID: {id}");
            }

            // If no student is found, return null
            return student == null ? null : _mapper.Map<StudentDto>(student);
        }
        public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync(); 
    }

        // Update a student
        public async Task UpdateAsync(student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.StudentId); // Ensure 'StudentId' matches the DTO's property
            if (existingStudent != null)
            {
                // Map updated DTO values to the existing entity
                existingStudent.StudentId = student.StudentId;
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.Age = student.Age;
                existingStudent.DeptId = student.DeptId;
                existingStudent.CourseId = student.CourseId;
                existingStudent.DateOfBirth = student.DateOfBirth;
               // existingStudent.EnrollmentDate = student.EnrollmentDate;
                existingStudent.Email = student.Email;
                existingStudent.Salary = student.Salary;
                existingStudent.UserName = student.UserName;

                // If password is not empty, update it
                if (!string.IsNullOrEmpty(student.Password))
                {
                    existingStudent.Password = student.Password; // Ensure it's hashed elsewhere
                }

                _context.Students.Update(existingStudent);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<List<StudentDto>> FilterStudentsSP(string query)
        {
            // Define the parameters for the stored procedure
            var parameters = new[]
            {
        new SqlParameter("@SEARCH_TEXT", query ?? (object)DBNull.Value),
        new SqlParameter("@SORT_COLUMN_NAME", "Name"), // column name, change as needed
        new SqlParameter("@SORT_COLUMN_DIRECTION", "ASC"), //  direction, change as needed
        new SqlParameter("@START_INDEX", 0), // starting index, change as needed
        new SqlParameter("@PAGE_SIZE", 10) //  page size, change as needed
    };

            // Execute the stored procedure using FromSqlRaw
            var students = await _context.Students
                .FromSqlRaw("EXEC dbo.FilterStudent @SEARCH_TEXT, @SORT_COLUMN_NAME, @SORT_COLUMN_DIRECTION, @START_INDEX, @PAGE_SIZE", parameters)
                .ToListAsync();

            // Map students to DTOs using AutoMapper
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return studentDtos;
        }
        public async Task<List<StudentDto>> GetStudentsBySP(string query, int pageno, int pagesize)
        {


            var students = _context.Students
                   .FromSqlRaw("EXEC dbo.FilterByName @NamePart",
                               new SqlParameter("@NamePart", query ?? (object)DBNull.Value))
                   .ToList();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return studentDtos;
        }

        //-- another way 

        //    public async Task<IEnumerable<StudentDto>> GetQueryAsync<StudentDto>(string sql, object parameters = null)
        //where StudentDto : new()
        //    {
        //        // Execute raw SQL query and get Student entities
        //        var students = await _context.Students
        //            .FromSqlRaw(sql, parameters) // Execute query and get Student entities
        //            .ToListAsync();              // Execute asynchronously and get the list

        //        // Map the list of Student entities to StudentDto
        //        var studentDtos = _mapper.Map<List<StudentDto>>(students);

        //        return studentDtos; // Return the list of mapped StudentDto objects
        //    }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync(
              int PageNumber = 1,
              int? PageSize = null,
              string SearchTerm = "",
              string SortBy = "deptName",
              string SortOrder = "ASC",
              int[]? DeptIds = null,
              string DeptName = null,
              int? Age = null,
              int? AgeMin = null,
              int? AgeMax = null,
            string CourseName = null,
            int? SalaryMin=null,
            int? SalaryMax= null
)
        {
           
            // Define the parameters for the stored procedure
            var parameters = new[]
              {
                new SqlParameter("@PageNumber", SqlDbType.Int) {Value=PageNumber },
                new SqlParameter("@PageSize", SqlDbType.Int) { Value = PageSize?? (object)DBNull.Value },
                new SqlParameter("@SearchTerm", SqlDbType.NVarChar) { Value = SearchTerm ?? (object)DBNull.Value },
                new SqlParameter("@SortBy", SqlDbType.NVarChar) { Value = SortBy },
                new SqlParameter("@SortOrder", SqlDbType.NVarChar) { Value = SortOrder },
                new SqlParameter("@DeptId", SqlDbType.NVarChar) { Value = DeptIds != null && DeptIds.Count() > 0 ? string.Join(",", DeptIds) : DBNull.Value },
                new SqlParameter("@DeptName",SqlDbType.NVarChar){Value=(object)DBNull.Value},
                new SqlParameter("@Age", SqlDbType.NVarChar) { Value = (object)Age ?? DBNull.Value },
                new SqlParameter("@AgeMin", SqlDbType.Int) { Value = (object)AgeMin ?? DBNull.Value },
                new SqlParameter("@AgeMax", SqlDbType.Int) { Value = (object)AgeMax ?? DBNull.Value },
                                new SqlParameter("@CourseName",SqlDbType.NVarChar){Value=(object)DBNull.Value},
                                new SqlParameter("@SalaryMin",SqlDbType.Int){Value=(object)SalaryMin ?? DBNull.Value }, 
                                new SqlParameter("@SalaryMax",SqlDbType.Int){ Value=(object)SalaryMax?? DBNull.Value}

            };

            // Execute the stored procedure using FromSqlRaw
            var students = await _context.Students
                .FromSqlRaw("EXEC dbo.GetStudents @PageNumber, @PageSize, @SearchTerm, @SortBy, @SortOrder, @DeptId, @DeptName ,@Age, @AgeMin, @AgeMax,@CourseName,@SalaryMin,@SalaryMax", parameters)
                .ToListAsync();

            // Map students to DTOs using AutoMapper

            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return studentDtos;

        }

        public async Task<int> GetStudentsCount( string SearchTerm, 
                int[]? DeptIds, string DeptName, int? Age, int? AgeMin, int? AgeMax, string CourseName, int? SalaryMin , int? SalaryMax ) 
        {
            // Define the parameters for the stored procedure
            var parameters = new[]
              {
                
                new SqlParameter("@SearchTerm", SqlDbType.NVarChar) { Value = SearchTerm ?? (object)DBNull.Value },
                
                new SqlParameter("@DeptId", SqlDbType.NVarChar) { Value = DeptIds != null && DeptIds.Count() > 0 ? string.Join(",", DeptIds) : DBNull.Value },
                new SqlParameter("@DeptName",SqlDbType.NVarChar){Value=(object)DBNull.Value},
                new SqlParameter("@Age", SqlDbType.NVarChar) { Value = (object)Age ?? DBNull.Value },
                new SqlParameter("@AgeMin", SqlDbType.Int) { Value = (object)AgeMin ?? DBNull.Value },
                new SqlParameter("@AgeMax", SqlDbType.Int) { Value = (object)AgeMax ?? DBNull.Value },
                                new SqlParameter("@CourseName",SqlDbType.NVarChar){Value=(object)DBNull.Value},
                                new SqlParameter("@SalaryMin",SqlDbType.Int){Value=(object)SalaryMin ?? DBNull.Value },
                                new SqlParameter("@SalaryMax",SqlDbType.Int){ Value=(object)SalaryMax?? DBNull.Value}

            };
            // Counting the number of students asynchronously
            var query =  await _context.ClaimDataView
               .FromSqlRaw("EXEC dbo.GetTotalRecords  @SearchTerm, @DeptId, @DeptName ,@Age, @AgeMin, @AgeMax,@CourseName,@SalaryMin,@SalaryMax", parameters)
                .ToListAsync();
            var test = Convert.ToInt32(query[0].count);



            return test;

        }
    }
}



