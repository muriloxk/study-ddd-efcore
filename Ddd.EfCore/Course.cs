using System;
using System.Linq;

namespace Ddd.EfCore
{
    //Enumeration Pattern
    public class Course : Entity
    {
        public static readonly Course Calculus = new Course("Calculus");
        public static readonly Course Chemistry = new Course("Chemistry");
        public static readonly Course[] AllCourses = { Calculus, Chemistry };

        protected Course() { }

        private Course(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }

        public static Course FromId(Guid id)
        {
            return AllCourses.SingleOrDefault(x => x.Id == id);
        }
    }
}
