namespace Ddd.EfCore
{
    public class Course
    {
        public Course(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
    }
}
