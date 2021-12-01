

using Microsoft.EntityFrameworkCore;
using University.Persistance.Context;

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
        }
    }
}
