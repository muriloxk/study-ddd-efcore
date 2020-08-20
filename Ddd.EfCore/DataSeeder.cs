using System;
namespace Ddd.EfCore
{
    public class DataSeeder
    {
        public static void Seed(SchoolContext context)
        {
            var courseOne = new Course(1L, "Course One");
            var courseTwo = new Course(2L, "Course Two");

            context.Students.Add(new Student(1L, "Murilo", "murilando@gmail.com", courseOne));
            context.Students.Add(new Student(2L, "Pedro", "pedroperez@gmail.com", courseOne));
            context.Students.Add(new Student(3L, "Leonardo", "leonardomancuso@gmail.com", courseTwo));

            context.SaveChanges();
        }
    }
}
