using AutoMapper;
using MediatR;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Application.Features.Account.Queries.GetRefreshAccessToken
{
    public class GetRefreshAccessTokenQuery : IRequest<RefreshTokenResponse>
	{

	}

	public class GetRefreshAccessTokenQueryHandler : IRequestHandler<GetRefreshAccessTokenQuery, RefreshTokenResponse>
	{

		private readonly IAccountService _accountService;

		public GetRefreshAccessTokenQueryHandler(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<RefreshTokenResponse> Handle(GetRefreshAccessTokenQuery request, CancellationToken cancellationToken)
		{
			RefreshTokenResponse response = new();

			var result = _accountService.ValidateRefreshToken();

			if(result.Contains("Error") || result == "")
			{
				response.HasError = true;
				return response;
			}

			var refresh = await _accountService.GenerateJWToken(result);
			response.JWToken = refresh;

			return response;
		}

	}
}
