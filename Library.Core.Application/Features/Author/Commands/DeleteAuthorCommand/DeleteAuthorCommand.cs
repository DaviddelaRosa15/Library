using AutoMapper;
using Library.Core.Application.Dtos.Author;
using Library.Core.Application.Interfaces.Reposirories;
using Library.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Core.Application.Features.Author.Commands.DeleteAuthorCommand
{
    public class DeleteAuthorCommand : IRequest<AuthorDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de especificar el id del autor.")]
        public string Id { get; set; }
    }

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            AuthorDTO response = new();

            try
            {
                var author = await _authorRepository.GetByIdAsync(command.Id);

                if (author == null)
                {
                    throw new Exception("No se encontró ese autor en la biblioteca");
                }

                await _authorRepository.DeleteAsync(author);

                response = _mapper.Map<AuthorDTO>(author);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

    }
}
