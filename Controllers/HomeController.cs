using System.Diagnostics;
using Crudbyme.Dtos;
using Crudbyme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crudbyme.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Crudbyme.Migrations;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Crudbyme.Controllers
{
 // [Authorize]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourse _courseRepository;
        private readonly IDepartment _departmentRepository;

        public HomeController(IStudentRepository studentrepository, ICourse courseRepository, IDepartment departmentRepository)
        {
            _studentRepository = studentrepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index(int PageNumber = 1, int? PageSize = null, string SearchTerm = null,
       string SortBy = "FullName", string SortOrder = "ASC", int[]? DeptIds = null, string DeptName = null, int? Age = null,
       int? AgeMin = null, int? AgeMax = null, string CourseName = "", int? SalaryMin=null, int? SalaryMax = null   , int ? selectedAge=null)
        {

            //ViewData["SortOrder"] = SortOrder== "ASC"? "DESC" : "ASC";
            if(SortOrder.Trim() == "ASC")
            {
                ViewData["SortOrder"] = "DESC"; 
            }
            else
            {
                ViewData["SortOrder"] = "ASC";
            }
                SortOrder = ViewData["SortOrder"].ToString();
            
            var students = await _studentRepository.GetStudentsAsync(PageNumber, PageSize, SearchTerm, SortBy, SortOrder, DeptIds, DeptName, Age, AgeMin,AgeMax,CourseName, SalaryMin,SalaryMax);
            var courses = await _courseRepository.GetCourse();
            var departments = await _departmentRepository.GetDepartment();

// i have kept this logic for getting my dropdowm
            foreach (var student in students)
            {
                student.CourseName = courses.FirstOrDefault(c => c.CourseId == student.CourseId)?.CourseName;
                student.DepartmentName = departments.FirstOrDefault(d => d.DeptId == student.DeptId)?.DeptName;
            }

            // it gets the student total count by which we do calculation for page size and page number ahead in the program(doosra procedure call krk  store kraya h variable m)
            var totalStudents = await _studentRepository.GetStudentsCount( SearchTerm,DeptIds,DeptName,Age, AgeMin, AgeMax,CourseName,SalaryMin, SalaryMax);

            // Calculate the total number of pages also handling the pagesize getting null view 
            int totalPages = PageSize==null?1:( (int)Math.Ceiling((double)totalStudents / (double)PageSize));   

            // Ensuring  PageNumber is in valid range
            var FirstPage = 1;
            var LastPage = totalPages;
            var Page = Math.Max(FirstPage, Math.Min(PageNumber, totalPages));

          ////SELECTING DEPARTMENT DATA FROM DROP DOWN ------------------------------
            var departmentDDl = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.DeptId.ToString()
            }).ToList();
             // logic  for my pagination dropdown(variable lia-->enumerate kia-->range dali-->
             //select function lia -->text ,  value dali, current page =selctpage=pagesize kia-->or list  m krdia )
            var paginationDDl = Enumerable.Range(1, totalStudents)
       .Select(page => new SelectListItem
       {
           Text = page.ToString(),
           Value = page.ToString(),
           Selected = page == PageSize //  it is Selects the current page
       })
       .ToList();
            var pagesizeddl = new List<SelectListItem>()
            {
                new SelectListItem{Text = "10", Value = "10" },
                new SelectListItem{Text = "15", Value = "15" },
                new SelectListItem{Text = "20", Value = "20" },
                new SelectListItem{Text = "25", Value = "25" },
                new SelectListItem{Text = "30", Value = "30" },
                                new SelectListItem{Text = "all", Value = "null" },

                 //  it is Selects the current page
            };
            // Pass this list to ViewBag
            //ViewBag.PaginationDDl = paginationDDl;
            ViewBag.MinAge = AgeMin;
            ViewBag.MaxAge = AgeMax;

            ViewBag.AgeMin = AgeMin;
            ViewBag.AgeMax = AgeMax;
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = PageNumber;
            // Passing results to the view using ViewBag
            ViewBag.SearchTerm = SearchTerm;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageNumber = PageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = PageSize;
            ViewBag.FirstPage = FirstPage;
            ViewBag.LastPage = LastPage;
            ViewBag.Page = Page;
            ViewBag.Age = Age;
            ViewBag.Pagesizeddl = pagesizeddl;
            ViewBag.TotalStudents = totalStudents;
            //ViewData["SelectedDepartments"] = DeptIds;
            ViewBag.DeptIds = DeptIds;

            ViewBag.Departments = departmentDDl;
            ViewBag.SelectedAge = selectedAge;
            ViewBag.SalaryMin = SalaryMin;
            ViewBag.SalaryMax = SalaryMax;

            return View(students);

        }

       

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepository.GetDepartment();
            ViewBag.Departments = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.DeptId.ToString()
            }).ToList();

            var courses = await _courseRepository.GetCourse();
            ViewBag.Courses = courses.Select(x => new SelectListItem
            {
                Text = x.CourseName,
                Value = x.CourseId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                var student = new student
                {


                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Age = studentDto.Age,
                    Salary = studentDto.Salary,
                    Email = studentDto.Email,
                    StudentId = studentDto.StudentId,
                    EnrollmentDate = studentDto.EnrollmentDate,
                    DateOfBirth = studentDto.DateOfBirth,   
                };

                await _studentRepository.AddAsync(studentDto);
                TempData["insert_success"] = "Created successfully ";

                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Login", "Admin");
            }

            var departments = await _departmentRepository.GetDepartment();
            ViewBag.Departments = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.DeptId.ToString()
            }).ToList();

            var courses = await _courseRepository.GetCourse();
            ViewBag.Courses = courses.Select(x => new SelectListItem
            {
                Text = x.CourseName,
                Value = x.CourseId.ToString()
            }).ToList();

            return View(studentDto);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentRepository.GetByIdAsync(id.Value);
            if (student == null)
            {
                Console.WriteLine("Student not found with the given id.");
                return NotFound();
            }

            var department = await _departmentRepository.GetByIdAsync(Convert.ToInt32(student.DeptId));
            var course = await _courseRepository.GetByIdAsync(Convert.ToInt32(student.CourseId));

            if (department == null)
                Console.WriteLine("Department not found.");
            if (course == null)
                Console.WriteLine("Course not found.");

            var studentDto = new StudentDto
            {
                StudentId = student.StudentId,
                FullName=student.FullName,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                UserName = student.UserName,
                Password = student.Password,
                CourseId = student.CourseId,
                DeptId = student.DeptId,
                Email = student.Email,
                Salary = student.Salary,
                DateOfBirth = student.DateOfBirth,
                EnrollmentDate = student.EnrollmentDate,
                CourseName = course?.CourseName ?? "Course not available",  //if null
                DepartmentName = department?.DeptName ?? "Department not available"  //  if null
            };

            return View(studentDto);
        }
        public JsonResult GetCoursesByDepartment(int deptId)
        {
            var courses = _courseRepository.GetCoursesByDepartment(deptId)
                                            .Select(c => new { c.CourseId, c.CourseName })
                                            .ToList();

            // Return the filtered courses as JSON
            return Json(courses);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var studentDto = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,

                Email = student.Email,
                Salary = student.Salary,
                DeptId = student.DeptId,
                CourseId = student.CourseId,
                Age = student.Age,
                DateOfBirth= student.DateOfBirth,
              //  EnrollmentDate=student.EnrollmentDate,
                UserName=student.UserName,
                Password=student.Password,


            };
            var departments = await _departmentRepository.GetDepartment();
            ViewBag.Departments = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.DeptId.ToString()
            }).ToList();

            var courses = await _courseRepository.GetCourse();
            ViewBag.Courses = courses.Select(x => new SelectListItem
            {
                Text = x.CourseName,
                Value = x.CourseId.ToString()
            }).ToList();


            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentRepository.GetQuery()
                                                       .FirstOrDefaultAsync(x => x.StudentId == id);
                if (student == null)
                {
                    return NotFound();
                }
                if (!string.IsNullOrEmpty(student.Password))
                {
                    // Hash the new password
                    student.Password = studentDto.Password;

                }

                // Save the student details


                student.StudentId = studentDto.StudentId;
                student.FirstName = studentDto.FirstName;
                student.LastName = studentDto.LastName;
               
               student.Age = studentDto.Age;
                student.DeptId = studentDto.DeptId;
                student.CourseId=studentDto.CourseId;
                student.DateOfBirth = studentDto.DateOfBirth;
            //    student.EnrollmentDate = studentDto.EnrollmentDate;
                student.Email = studentDto.Email;
                student.Salary = studentDto.Salary;
                student.UserName = studentDto.UserName;

                await _studentRepository.UpdateAsync(student);
                TempData["update_success"] = "updated successfully ";

                return RedirectToAction(nameof(Index));
            }

            return View(studentDto);
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                Console.WriteLine("Student not found with the given id.");
                return NotFound();
            }

            var department = await _departmentRepository.GetByIdAsync(Convert.ToInt32(student.DeptId));
            var course = await _courseRepository.GetByIdAsync(Convert.ToInt32(student.CourseId));

            // Log department and course data
            if (department == null)
                Console.WriteLine("Department not found.");
            if (course == null)
                Console.WriteLine("Course not found.");

            var studentDto = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                FullName=student.FullName,
                Age = student.Age,
                UserName = student.UserName,
                Password = student.Password,
                CourseId = student.CourseId,
                DeptId = student.DeptId,
                Email = student.Email,
                Salary = student.Salary,
                DateOfBirth = student.DateOfBirth,
                EnrollmentDate = student.EnrollmentDate,
                CourseName = course?.CourseName ?? "Course not available",  // Fallback if null
                DepartmentName = department?.DeptName ?? "Department not available"  // Fallback if null
            };

            return View(studentDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            await _studentRepository.DeleteAsync(id);
            TempData["delete_success"] = "Deleted successfully ";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Course()
        {
            // Fetch the list of courses from the repository
            var courses = await _courseRepository.GetCourse();


            // Pass the list of courses to the view
            return View(courses);
        }
        public async Task<IActionResult> Departments()
        {
            // Fetch the list of courses from the repository
            var departments= await _departmentRepository.GetDepartment();


            // Pass the list of courses to the view
            return View(departments);
        }
      //  [Authorize]
        public IActionResult Welcome() { 
        
        return View();
        }
        public IActionResult Logout()

        {
            return View();
            return RedirectToAction("Login", "Admin");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
