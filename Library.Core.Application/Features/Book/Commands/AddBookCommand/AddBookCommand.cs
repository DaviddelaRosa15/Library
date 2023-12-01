using AutoMapper;
using Library.Core.Application.Dtos.Book;
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

namespace Library.Core.Application.Features.Book.Commands.AddBookCommand
{
    public class AddBookCommand : IRequest<BookDTO>
    {
        [SwaggerParameter(Description = "Titulo")]
        [Required(ErrorMessage = "Debe de especificar un titulo para este libro.")]
        public string Title { get; set; }

        [JsonIgnore]
        public string? AuthorId { get; set; }
    }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDTO>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            BookDTO response = new();

            try
            {
                var newBook = _mapper.Map<Domain.Entities.Book>(command);
                var book = await _bookRepository.AddAsync(newBook);
                response = _mapper.Map<BookDTO>(book);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

    }
}
