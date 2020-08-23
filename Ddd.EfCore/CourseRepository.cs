using System;
namespace Ddd.EfCore
{
    public class CourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public Course GetById(Guid courseId)
        {
            return _context.Courses.Find(courseId);
        }
    }
}
