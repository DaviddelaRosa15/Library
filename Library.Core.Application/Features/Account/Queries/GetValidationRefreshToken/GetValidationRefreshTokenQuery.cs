using MediatR;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Features.Account.Queries.GetValidationRefreshToken
{
    public class GetValidationRefreshTokenQuery : IRequest<GetValidationRefreshTokenQueryResponse>
    {
    }

    public class GetValidationRefreshTokenQueryHandler : IRequestHandler<GetValidationRefreshTokenQuery, GetValidationRefreshTokenQueryResponse>
    {
        private readonly IAccountService _accountService;

        public GetValidationRefreshTokenQueryHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }


        public async Task<GetValidationRefreshTokenQueryResponse> Handle(GetValidationRefreshTokenQuery command, CancellationToken cancellationToken)
        {
            GetValidationRefreshTokenQueryResponse response = new();
            try
            {
                var result = _accountService.ValidateRefreshToken();
                response.ValidRefreshToken = result != "" && !result.Contains("Error");
                return response;
            }
            catch (Exception ex)
            {
                response.ValidRefreshToken = false;
                return response;
            }
        }

    }
}
