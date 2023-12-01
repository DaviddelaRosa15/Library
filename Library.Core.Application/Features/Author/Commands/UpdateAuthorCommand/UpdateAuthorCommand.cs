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

namespace Library.Core.Application.Features.Author.Commands.UpdateAuthorCommand
{
    public class UpdateAuthorCommand : IRequest<AuthorDTO>
    {
        [SwaggerParameter(Description = "Id del autor a actualizar.")]
        [Required(ErrorMessage = "Debe de especificar el id del autor.")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de especificar un nombre para este libro.")]
        public string Name { get; set; }
    }

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            AuthorDTO response = new();

            try
            {
                var author = await _authorRepository.GetByIdAsync(command.Id);
                
                if (author == null)
                {
                    throw new Exception("No se encontró ese autor en la biblioteca");
                }

                author.Name = command.Name;
                await _authorRepository.UpdateAsync(author,author.Id);

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
