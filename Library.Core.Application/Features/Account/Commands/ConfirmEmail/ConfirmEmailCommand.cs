using AutoMapper;
using MediatR;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Application.Features.Account.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<ConfirmEmailResponse>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

	public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailResponse>
	{
		private readonly IAccountService _accountService;

		public ConfirmEmailCommandHandler(IAccountService accountService, IMapper mapper)
		{
			_accountService = accountService;
		}


		public async Task<ConfirmEmailResponse> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
		{
			ConfirmEmailResponse response = new();
			try
			{
				response = await _accountService.ConfirmEmailAsync(command.UserId, command.Token);
				return response;
			}
			catch (Exception)
			{
				response.HasError = true;
				response.Error = "Ocurrió un error tratando de confirmar la cuenta para el usuario.";
				return response;
			}
		}

	}
}
