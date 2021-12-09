using Microsoft.EntityFrameworkCore;
using University.Persistance.Context;
using University.Persistance.Entities.Students;

namespace University
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                 .AddControllersAsServices();
            services.AddControllers();
            services.AddTransient<StudentService>();

            var connectionString = Configuration.GetConnectionString("database");
            services.AddDbContextPool<UniversityDbContext>(option =>
            option.UseSqlServer(connectionString,
             contextOptions =>
             {
                 contextOptions.MigrationsAssembly(typeof(UniversityDbContext).Assembly.FullName);
             })
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var runMigration = true;
            if (runMigration)
            {
                using var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();
                using var context = serviceScope.ServiceProvider.GetService<UniversityDbContext>();
                context.Database.Migrate();
                SeedDatabase(context);
            }
        }
        private static void SeedDatabase(UniversityDbContext context)
        {
            var courses = new List<Course>()
            {
                new Course()
                {
                   Name = "Calculus",
                   Credits = 3
                },
                  new Course()
                {
                   Name = "Chemistry",
                   Credits = 3
                },
                    new Course()
                {
                   Name = "Composition",
                   Credits = 3
                },
                      new Course()
                {
                   Name = "Literature",
                   Credits = 4
                },
                        new Course()
                {
                   Name = "Trigonometry",
                   Credits = 4
                },
                          new Course()
                {
                   Name = "Microeconomics",
                   Credits = 3
                },
                            new Course()
                {
                   Name = "Macroeconomics",
                   Credits = 3
                },
            };

            if (!context.Courses.Any())
            {
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }
        }
    }
}
