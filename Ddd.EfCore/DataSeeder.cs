using System;
namespace Ddd.EfCore
{
    public class DataSeeder
    {
        public static void Seed(SchoolContext context)
        {
            var studentMurilo = new Student("Murilo", Email.Create("murilando@gmail.com").Value, Course.Calculus) { NameValueObject = Name.Create("Murilo", "Sanches", Suffix.Sr).Value };
            var studentJoao = new Student("João", Email.Create("joao@gmail.com").Value, Course.Calculus) { NameValueObject = Name.Create("João", "Sanches", Suffix.Sr).Value }; 
            var studentJose = new Student("José", Email.Create("jose@gmail.com").Value, Course.Chemistry) { NameValueObject = Name.Create("José", "Sanches", Suffix.Sr).Value }; 

            
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
