using Library.Core.Domain.Settings;
using Library.Core.Application.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Interfaces.Services
{
	public interface IEmailService
	{
		public MailSettings _mailSettings { get; }
		Task SendAsync(EmailRequest request);
	}
}
