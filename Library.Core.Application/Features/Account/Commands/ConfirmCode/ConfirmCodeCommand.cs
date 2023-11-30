using MediatR;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Application.Features.Account.Commands.ConfirmCode

{
    public class ConfirmCodeCommand : IRequest<ConfirmCodeResponse>
	{
        [SwaggerParameter(Description = "Código de confirmación")]
        [Required(ErrorMessage = "Debe de ingresar el código que se le envió al correo")]
        public string Code { get; set; }
    }

	public class ConfirmCodeCommandHandler : IRequestHandler<ConfirmCodeCommand, ConfirmCodeResponse>
	{
		private readonly IAccountService _accountService;

		public ConfirmCodeCommandHandler(IAccountService accountService)
		{
			_accountService = accountService;
		}


		public async Task<ConfirmCodeResponse> Handle(ConfirmCodeCommand command, CancellationToken cancellationToken)
		{
			ConfirmCodeResponse response = new();
			try
			{
				response = _accountService.ConfirmCode(command.Code);
				return response;
			}
			catch (Exception ex)
			{
				response.HasError = true;
				response.Error = "Ocurrió un error.";
				return response;
			}
		}

	}
}
