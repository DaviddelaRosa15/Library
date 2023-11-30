using Library.Core.Domain.Settings;
using Library.Core.Application.Interfaces.Services;
using Library.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Shared
{
	public static class ServiceRegistration
	{
		public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
			services.AddTransient<IEmailService, EmailService>();
		}
	}
}
