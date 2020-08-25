using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ddd.EfCore
{
    public class StudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
           var student = await _context.Students.FindAsync(studentId);
           await _context.Entry(student).Collection(x => x.Enrollments).LoadAsync();

           return student;
        }

        public void Save(Student student)
        {
            _context.Attach(student);
        }
    }
}
