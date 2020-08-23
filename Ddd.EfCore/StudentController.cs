using System;
using System.Linq;

namespace Ddd.EfCore
{
    //Simulating a web api controller

    public class StudentController
    {

        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        public string CheckStudentFavoriteCourse(Guid studentId, Guid courseId)
        {
            Student student = _context.Students.Find(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Student not found";

           return student.FavoriteCourse == course ? "Yes" : "No";
        }

        public string AddEnrollment(Guid studentId, Guid courseId, Grade grade)
        {
            Student student = _context.Students.Find(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Course not found";

            _context.Attach(course);
            student.EnrollIn(course, grade);
            _context.SaveChanges();

            return "OK";
        }
    }
}
