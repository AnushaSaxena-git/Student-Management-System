using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crudbyme.Models
{
    public class Course
    {
        [Key]                                                                                                                       
       
        public int CourseId { get; set; }
       public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Department")   ]
        public int DeptId {  get; set; }        
        public Department Department { get; set; }  
        public ICollection<StudentCourses> StudentCourses { get; set; } // Navigation property to junction table
    }
}
    
