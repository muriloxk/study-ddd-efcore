using System;
using System.Threading.Tasks;

namespace Ddd.EfCore
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = GetConnectionString();
            Seed(connectionString);

            //await Execute(studentController => studentController.TestValueObject());

            var responseEditPersonalInfoAsync = await Execute(studentController => studentController.EditPersonalInfoAsync(TestsConfig.PrimaryKeys["studentMurilo"], "Carlos", "carlos@hotmail.com", Course.Chemistry.Id));
            Console.WriteLine("responseEditPersonalInfoAsync: " + responseEditPersonalInfoAsync);

            var responseRegisterStudent = await Execute(studentController =>  studentController.RegisterStudentAsync("Carlos", "carlos@hotmail.com", Course.Calculus.Id));
            Console.WriteLine("responseRegisterStudent: " + responseRegisterStudent);

            var responseCheckStudent = await Execute(studentController =>  studentController.CheckStudentFavoriteCourse(TestsConfig.PrimaryKeys["studentMurilo"], Course.Calculus.Id));
            Console.WriteLine("responseCheckStudent: " + responseCheckStudent);

            var responseAddEnrollment = await Execute(studentController =>  studentController.AddEnrollment(TestsConfig.PrimaryKeys["studentMurilo"], Course.Chemistry.Id, Grade.A));
            Console.WriteLine("responseAddEnrollment: " + responseAddEnrollment);

            var responseDisenroll = await Execute(studentController =>  studentController.Disenrollment(TestsConfig.PrimaryKeys["studentMurilo"], Course.Calculus.Id));
            Console.WriteLine("responseDisenroll: " + responseDisenroll);

            Console.ReadKey();
        }

        private static async Task<string> Execute(Func<StudentController,Task<string>> func)
        {
            var connectionString = GetConnectionString();

            using (var context = new SchoolContext(connectionString, true, GetEventDispatcher()))
            {
                var controller = new StudentController(context);
                return await func(controller);
            }
        }

        private static void Seed(string connectionString)
        {
            using (var context = new SchoolContext(connectionString, true, GetEventDispatcher()))
            {
                DataSeeder.Seed(context);
            }
        }

        private static EventDispatcher GetEventDispatcher()
        {
            IBus bus = new Bus();
            var messageBus = new MessageBus(bus);
            var eventDispatcher = new EventDispatcher(messageBus);

            return eventDispatcher;
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
