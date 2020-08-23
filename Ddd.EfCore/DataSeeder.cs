using System;
namespace Ddd.EfCore
{
    public class DataSeeder
    {
        public static void Seed(SchoolContext context)
        {
            var studentMurilo = new Student("Murilo", "murilando@gmail.com", Course.Calculus);
            var studentJoao = new Student("João", "joao@gmail.com", Course.Calculus);
            var studentJose = new Student("José", "jose@gmail.com", Course.Chemistry);

            TestsConfig.PrimaryKeys.Add("studentMurilo", studentMurilo.Id);
            TestsConfig.PrimaryKeys.Add("studentJoao", studentJoao.Id);
            TestsConfig.PrimaryKeys.Add("studentJose", studentJose.Id);

            studentMurilo.AddSubject(new Subject("Subject 1"));
            studentMurilo.AddSubject(new Subject("Subject 2"));

            context.Students.Add(studentMurilo);
            context.Students.Add(studentJoao);
            context.Students.Add(studentJose);

            context.SaveChanges();
        }
    }
}
