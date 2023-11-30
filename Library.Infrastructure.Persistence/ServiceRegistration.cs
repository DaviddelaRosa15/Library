using Library.Core.Application.Interfaces.Repositories;
using Library.Infrastructure.Persistence.Contexts;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			var connection = configuration.GetConnectionString("PostgreSQL");
            var password = configuration["PASSWORD"];
            var host = configuration["HOST"];
            var database = configuration["DATABASE"];
            connection = connection.Replace("#", password);
			connection = connection.Replace("ServerHost", host);
			connection = connection.Replace("DataBank", database);

            #region Vaciar tablas
            /*var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(connection, m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            var context = new ApplicationContext(optionsBuilder.Options);
			context.TruncateTables();*/
			#endregion

			#region Contexts
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("MediSearchDb"));
			}
			else
			{
				services.AddDbContext<ApplicationContext>(options =>
				{
					options.EnableSensitiveDataLogging();
					options.UseNpgsql(connection,
					m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
				});
			}
			#endregion

			#region Repositories
			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			#endregion
		}
	}
}
