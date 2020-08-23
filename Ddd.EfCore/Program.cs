using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ddd.EfCore
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = GetConnectionString();
            Seed(connectionString);

            var responseCheckStudent =  Execute(studentController => studentController.CheckStudentFavoriteCourse(TestsConfig.PrimaryKeys["studentMurilo"], Course.Calculus.Id));
            Console.WriteLine("responseCheckStudent: " + responseCheckStudent);

            var responseAddEnrollment = Execute( studentController => studentController.AddEnrollment(TestsConfig.PrimaryKeys["studentMurilo"], Course.Chemistry.Id, Grade.A));
            Console.WriteLine("responseAddEnrollment: " + responseAddEnrollment);

            var responseDisenroll = Execute(studentController => studentController.Disenrollment(TestsConfig.PrimaryKeys["studentMurilo"], Course.Calculus.Id));
            Console.WriteLine("responseDisenroll: " + responseDisenroll);

            Console.ReadKey();
        }

        private static async Task<string> Execute(Func<StudentController,Task<string>> func)
        {
            var connectionString = GetConnectionString();

            using (var context = new SchoolContext(connectionString, true))
            {
                var controller = new StudentController(context);
                return await func(controller);
            }
        }

        private static void Seed(string connectionString)
        {
            using (var context = new SchoolContext(connectionString, true))
            {
                DataSeeder.Seed(context);
            }
        }

        private static string GetConnectionString()
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                        .SetBasePath(Directory.GetCurrentDirectory())
            //                        .AddJsonFile("appsettings.json")
            //                        .Build();

            //return configuration["ConnectionString"];

            return "MyDB";
        }
    }
}
