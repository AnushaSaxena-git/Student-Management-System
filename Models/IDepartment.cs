using Crudbyme.Dtos;

namespace Crudbyme.Models
{
    public interface IDepartment
    {
        Task<List<DepartmentDTO>> GetDepartment();
        Task<DepartmentDTO> GetByIdAsync(int id);
        Task SaveChangesAsync();

        //Task<List<DepartmentDTO>> GetDepartmentsBySP(string query);
    }
}
