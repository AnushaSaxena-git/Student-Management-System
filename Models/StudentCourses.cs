
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudbyme.Models {
   

        public class StudentCourses
        {
        
        [Key]
            public int StudentCoursesId { get; set; }
            [ForeignKey(nameof(student))]
            public int StudentId { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
            public student student { get; set; }

            

            public Course Course { get; set; }


        }
    }
