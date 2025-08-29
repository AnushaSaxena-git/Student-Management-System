using Crudbyme;
using Crudbyme.Models;
using Crudbyme.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  // For FromSqlRaw
using Microsoft.Data.SqlClient;


namespace Crudbyme.Models
{
    public interface IStudentRepository
    {
              Task<List<StudentDto>> GetStudents();
        Task<List<StudentDto>> GetStudentsBySP(string query);// basic stored procedure
        Task<List<StudentDto>> GetStudentsBySP(string query, int pageno, int pagesize=10);// basic paging 
      //  Task<List<StudentDto>> Functionalities(string query, int pageno, int pagesize, string searchTerm, string sortBy, string sortorder, int deptid, string deptname, int age, int param1, int param2);// all functionalities included 
        Task<List<StudentDto>> FilterStudentsSP(string query);// basic filter 

        Task<int> GetStudentsCount( string SearchTerm,
                int[]? DeptIds,string DeptName,int? Age, int? AgeMin, int? AgeMax, string CourseName, int? SalaryMin, int?SalaryMax);
        Task<StudentDto> GetByIdAsync(int id);
         Task SaveChangesAsync();

        Task AddAsync(StudentDto studentDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(student student);
        IQueryable<student> GetQuery();

        
        Task<IEnumerable<StudentDto>> GetStudentsAsync(
       int PageNumber,
       int? PageSize,
       string SearchTerm,
       string SortBy,
       string SortOrder,
       int[]? DeptIds,
       string DeptName,
       int? age,
       int? AgeMin,
       int? AgeMax,
            string CourseName,
            int? SalaryMin,
            int? SalaryMax);

    }
}
