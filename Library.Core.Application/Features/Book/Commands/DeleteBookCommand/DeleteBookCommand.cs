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

namespace Library.Core.Application.Features.Book.Commands.DeleteBookCommand
{
    public class DeleteBookCommand : IRequest<BookDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de especificar el id del libro.")]
        public string Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BookDTO>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            BookDTO response = new();

            try
            {
                var book = await _bookRepository.GetByIdAsync(command.Id);

                if (book == null)
                {
                    throw new Exception("No se encontró ese libro en la biblioteca");
                }

                await _bookRepository.DeleteAsync(book);

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
