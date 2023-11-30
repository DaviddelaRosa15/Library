using Library.Infrastructure.Identity.Entities;
using Library.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Presentation.WebApi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
            DotNetEnv.Env.Load();
			var app = CreateHostBuilder(args);

            app.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();
            });

            var host = app.Build();
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
                    #region Identity
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
					var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

					await DefaultRoles.SeedAsync(userManager, roleManager);
					await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
					#endregion

					#region Application

                    #endregion
                }
                catch (Exception ex)
				{

				}
			}
			
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}