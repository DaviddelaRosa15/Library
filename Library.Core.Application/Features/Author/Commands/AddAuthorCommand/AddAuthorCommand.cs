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

namespace Library.Core.Application.Features.Author.Commands.AddAuthorCommand
{
    public class AddAuthorCommand : IRequest<AuthorDTO>
    {
        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de especificar el nombre del autor.")]
        public string Name { get; set; }
    }

    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(AddAuthorCommand command, CancellationToken cancellationToken)
        {
            AuthorDTO response = new();

            try
            {
                var newAuthor = _mapper.Map<Domain.Entities.Author>(command);
                var author = await _authorRepository.AddAsync(newAuthor);
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
