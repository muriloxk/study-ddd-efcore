using System;
using System.Linq;

namespace Ddd.EfCore
{
    //Simulate a web api controller
    public class StudentController
    {
        private readonly SchoolContext _context;
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;

        public StudentController(SchoolContext context)
        {
            _context = context;
            _studentRepository = new StudentRepository(context);
            _courseRepository = new CourseRepository(context);
        }

        public string CheckStudentFavoriteCourse(Guid studentId, Guid courseId)
        {
            Student student = _studentRepository.GetById(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Student not found";

           return student.FavoriteCourse == course ? "Yes" : "No";
        }

        public string AddEnrollment(Guid studentId, Guid courseId, Grade grade)
        {
            Student student = _studentRepository.GetById(studentId);
            if (student == null)
                return "Student not found";

            Course course = _courseRepository.GetById(courseId);
            if (course == null)
                return "Course not found";

            student.EnrollIn(course, Grade.A);
            student.EnrollIn(course, Grade.A);

            _context.SaveChanges();

            //tests
            //var courses = _context.Courses.ToList();
            //var enrollments = _context.Enrollments.ToList();
            //var students = _context.Students.ToList();

            return "OK";
        }
    }
}
