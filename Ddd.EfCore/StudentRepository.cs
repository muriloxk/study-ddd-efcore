using System;
namespace Ddd.EfCore
{
    public class StudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student GetById(Guid studentId)
        {
           return _context.Students.Find(studentId);
        }

        public void Save(Student student)
        {
            _context.Students.Attach(student);
        }
    }
}
