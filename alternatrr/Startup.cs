using alternatrr.Configuration;
using alternatrr.Data;
using alternatrr.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading.Tasks;

namespace alternatrr
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration.GetSection("Login").Get<LoginConfiguration>());

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddDbContext<SonarrDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("sonarr")
                )
            );

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true
            ).AddEntityFrameworkStores<AppDbContext>();

            services.AddSingleton<SceneMappingService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);
            await InitializeLogin(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }


        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }

        private async Task InitializeLogin(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var loginConfig = serviceScope.ServiceProvider.GetRequiredService<LoginConfiguration>();
            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (string.IsNullOrEmpty(loginConfig.Username) || string.IsNullOrEmpty(loginConfig.Password)) throw new Exception("Missing login config");

            foreach (var identityUser in await appDbContext.Users.ToListAsync())
            {
                appDbContext.Users.Remove(identityUser);
            }
            await appDbContext.SaveChangesAsync();

            await userManager.CreateAsync(new IdentityUser(loginConfig.Username) { EmailConfirmed = true }, loginConfig.Password);
        }
    }
}