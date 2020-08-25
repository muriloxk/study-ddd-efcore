using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<string> RegisterStudentAsync(string name, string email, Guid favoriteCourseId)
        {
            Course favoriteCourse = await _courseRepository.GetByIdAsync(favoriteCourseId);
 
            if (favoriteCourse == null)
                return "Course not found";

            Result<Email> result = Email.Create(email);
            if (result.IsFailure)
                return result.Error;

     

            var student = new Student(name, result.Value, favoriteCourse);
            _studentRepository.Save(student);

            //tests
            EntityState entityState1 = _context.Entry(student).State;
            EntityState entityState2 = _context.Entry(student.FavoriteCourse).State;
            EntityState entityState3 = _context.Entry(student.Enrollments[0]).State;

            _context.SaveChanges();

            //tests
            var courses = _context.Courses.ToList();
            var enrollments = _context.Enrollments.ToList();
            var students = _context.Students.ToList();

            return "Ok";
        }

        //Should return error in version EFCore 3.1.7
        //Value objects with OwnsOne make:
        //Overcomplicated SQL
        //Can't share Value Object instances
        public async Task<string> TestValueObject()
        {
            var studentJoao = await _studentRepository.GetByIdAsync(TestsConfig.PrimaryKeys["studentJoao"]);
            var studentMurilo = await _studentRepository.GetByIdAsync(TestsConfig.PrimaryKeys["studentMurilo"]);

            studentJoao.NameValueObject = studentMurilo.NameValueObject;

            _context.Attach(studentJoao.NameValueObject.Suffix);
            _context.Attach(studentMurilo.NameValueObject.Suffix);
            _context.SaveChanges();

            return "OK";
        }

        public async Task<string> EditPersonalInfoAsync(Guid studentId,
                                                        string name,
                                                        string email,
                                                        Guid favoriteCourseId)
        {
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return "Student not found";

            Course favoriteCourse = await _courseRepository.GetByIdAsync(favoriteCourseId);
            if (favoriteCourse == null)
                return "Course not found";

            Result<Email> result = Email.Create(email);
            if (result.IsFailure)
                return result.Error;

            student.EditPersonalInfo(name, result.Value, favoriteCourse);
            _studentRepository.Save(student);

            //tests
            EntityState entityState1 = _context.Entry(student).State;
            EntityState entityState2 = _context.Entry(student.FavoriteCourse).State;
            EntityState entityState3 = _context.Entry(student.Enrollments[0]).State;

            _context.SaveChanges();

            //tests
            var courses = _context.Courses.ToList();
            var enrollments = _context.Enrollments.ToList();
            var students = _context.Students.ToList();

            return "Ok";
        }
    }
}
