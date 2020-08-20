using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ddd.EfCore
{
    public class SchoolContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;

        public DbSet<Student> Students { get; set; }

        public SchoolContext(string connectionString, bool useConsoleLogger)
        {
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseID");
                x.Property(p => p.Name);
            });

            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentID");
                x.Property(p => p.Email);
                x.Property(p => p.Name);
                x.HasOne(p => p.FavoriteCourse).WithMany();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = CriarLogger();

            optionsBuilder.UseLazyLoadingProxies()
                           .UseInMemoryDatabase(_connectionString);
                          

            if(_useConsoleLogger)
            {
                optionsBuilder.UseLoggerFactory(loggerFactory)
                              .EnableSensitiveDataLogging();
            }
        }

        private ILoggerFactory CriarLogger()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                                   && level == LogLevel.Information)
                       .AddConsole();
            });
        }
    }
}
