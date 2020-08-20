using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Ddd.EfCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Crmall";

            Seed(connectionString);

            using (var context = new SchoolContext(connectionString, true))
            {
                Student student = context.Students.Find(1L);
                Console.WriteLine(JsonSerializer.Serialize(student));
            }

            Console.ReadKey();
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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            return configuration["ConnectionString"];
        }
    }
}
