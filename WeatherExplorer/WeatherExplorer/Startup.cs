using Microsoft.EntityFrameworkCore;
using WeatherExplorer.DAL;
using WeatherExplorer.BL.Services;
using WeatherExplorer.BL.Services.Interfaces;

namespace WeatherExplorer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IFileReader, ExcelReader>();
            services.AddScoped<IViewer, WeatherViewer>();
            services.AddDbContext<WeatherExplorerContext>(context => context.UseNpgsql(Configuration.GetSection("ConnectionString").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Weather}/{action=GetWeather}/{id?}");
            });
        }
    }
}
