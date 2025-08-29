using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crudbyme.Dtos
{
    public class DepartmentDTO
    {
        [Display(Name = "Department ID")]

        public int DeptId { get; set; }



        [Display(Name ="Department Name")]

        public string DeptName { get; set; }
    }
}
