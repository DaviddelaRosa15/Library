using AutoMapper;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Helpers;
using Library.Core.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Features.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterResponse>
    {
        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar su nombre")]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "Apellido")]
        [Required(ErrorMessage = "Debe de ingresar su apellido")]
        public string LastName { get; set; }

        [SwaggerParameter(Description = "Teléfono")]
        [Required(ErrorMessage = "Debe de ingresar su telefono")]
        public string PhoneNumber { get; set; }

        [SwaggerParameter(Description = "Correo")]
        [Required(ErrorMessage = "Debe de ingresar su correo")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "Dirección")]
        [Required(ErrorMessage = "Debe de ingresar su dirección")]
        public string Address { get; set; }

        [SwaggerParameter(Description = "Foto de perfil")]
        [Required(ErrorMessage = "Debe de subir una foto suya")]
        public IFormFile Image { get; set; }

        [SwaggerParameter(Description = "Nombre de usuario")]
        [Required(ErrorMessage = "Debe de ingresar su nombre de usuario")]
        public string UserName { get; set; }

        [SwaggerParameter(Description = "Contraseña")]
        [Required(ErrorMessage = "Debe de ingresar su contraseña")]
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }


        public async Task<RegisterResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<RegisterRequest>(command);
                request.UrlImage = ImageUpload.UploadImageUser(command.Image);
                var response = await _accountService.RegisterUserAsync(request);

                if (response.HasError)
                {
                    ImageUpload.DeleteFile(request.UrlImage);
                }

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un error tratando de registrar el usuario.");
            }
        }

    }
}
