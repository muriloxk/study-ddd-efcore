using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ddd.EfCore
{
    public class SchoolContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Subject> Subjects { get; set; }

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
                x.Property(p => p.Id).ValueGeneratedNever()
                                     .HasColumnName("CourseID");
                x.Property(p => p.Name);
            });

            modelBuilder.Entity<Subject>(x =>
            {
                x.ToTable("Subject").HasKey(k => k.Id);
                x.Property(p => p.Id).ValueGeneratedNever()
                                     .HasColumnName("SubjectID");
                x.Property(p => p.Name);
            });

            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).ValueGeneratedNever()
                                     .HasColumnName("StudentID");
                x.Property(p => p.Email);
                x.Property(p => p.Name);

                x.HasOne(p => p.FavoriteCourse).WithMany();
                x.HasMany(p => p.Enrollments).WithOne(p => p.Student)
                                             .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);

                x.HasMany(p => p.Subjects).WithOne();
            });

            modelBuilder.Entity<Enrollment>(x =>
            {
                x.ToTable("Enrollment").HasKey(k => k.Id);
                x.Property(p => p.Id).ValueGeneratedNever()
                                     .HasColumnName("EnrollmentID");
                x.Property(p => p.Grade);

                x.HasOne(p => p.Student).WithMany(p => p.Enrollments);
                x.HasOne(p => p.Course);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = CriarLogger();

            optionsBuilder.UseLazyLoadingProxies()
            //optionsBuilder
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
