using System;
using System.Threading.Tasks;

namespace Ddd.EfCore
{
    public class CourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Course> GetByIdAsync(Guid courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }
    }
}
