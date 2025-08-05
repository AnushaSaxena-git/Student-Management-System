namespace Crudbyme.Dtos
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // Add DeptId and DeptName if you want them in the DTO
        public int DeptId { get; set; }

        public string DepartmentName { get; set; }
        // Add a property for Department Name

    }
}
