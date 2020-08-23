using System;
using System.Collections.Generic;
using System.Linq;

namespace Ddd.EfCore
{
    public class Student : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public virtual Course FavoriteCourse { get; private set; }

        private readonly List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        private readonly List<Subject> _subjects = new List<Subject>();
        public virtual IReadOnlyList<Subject> Subjects => _subjects.ToList();

        protected Student() { }

        public Student(string name,
                       string email,
                       Course favoriteCourse)
            : this()
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;

            EnrollIn(FavoriteCourse, Grade.A);
        }

        public void EnrollIn(Course course, Grade grade)
        {
            if (_enrollments.Any(x => x.Course == course))
                return;

            _enrollments.Add(new Enrollment(course, this, grade));
        }

        public void AddSubject(Subject subject)
        {
            _subjects.Add(subject);
        }

        public void Disenroll(Course course)
        {
            Enrollment enrollment = _enrollments.FirstOrDefault(x => x.Course == course);

            if (enrollment == null)
                return;

            _enrollments.Remove(enrollment);
        }
    }
}
