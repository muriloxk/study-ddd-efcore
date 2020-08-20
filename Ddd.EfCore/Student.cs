namespace Ddd.EfCore
{
    public class Student
    {
        protected Student() { }

        //The constructor has id as a parameter for testing only.
        public Student(long id,string name, string email, Course favoriteCourse)
            : this()
        {
            Id = id;
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public virtual Course FavoriteCourse { get; private set; }
    }
}
