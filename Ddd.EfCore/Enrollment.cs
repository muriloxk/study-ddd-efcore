namespace Ddd.EfCore
{
    public class Enrollment : Entity
    {

        public Grade Grade { get; private set; }
        public virtual Course Course { get; private set; }
        public virtual Student Student { get; private set; }

        public Enrollment()
        {
        }

        public Enrollment(Course course, Student student, Grade grade) : this()
        {
            Course = course;
            Student = student;
            Grade = grade;
        }
    }

    public enum Grade
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        F = 4
    };
}
