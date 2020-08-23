using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<string> CheckStudentFavoriteCourse(Guid studentId, Guid courseId)
        {
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Student not found";

           return student.FavoriteCourse == course ? "Yes" : "No";
        }

        public async Task<string> AddEnrollment(Guid studentId, Guid courseId, Grade grade)
        {
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return "Student not found";

            Course course = await _courseRepository.GetByIdAsync(courseId);
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


        public async Task<string> Disenrollment(Guid studentId, Guid courseId)
        {
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return "Student not found";

            Course course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                return "Course not found";

            student.Disenroll(course);
            _context.SaveChanges();

            //tests
            //var courses = _context.Courses.ToList();
            //var enrollments = _context.Enrollments.ToList();
            //var students = _context.Students.ToList();

            return "OK";
        }
    }
}
