﻿using AutoMapper;
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

namespace Library.Core.Application.Features.Book.Commands.UpdateBookCommand
{
    public class UpdateBookCommand : IRequest<BookDTO>
    {
        [SwaggerParameter(Description = "Id del libro a actualizar.")]
        [Required(ErrorMessage = "Debe de especificar el id del libro.")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Titulo")]
        [Required(ErrorMessage = "Debe de especificar un titulo para este libro.")]
        public string Title { get; set; }

        [SwaggerParameter(Description = "Autor")]
        [Required(ErrorMessage = "Debe de especificar un autor para este libro.")]
        public string AuthorId { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDTO>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            BookDTO response = new();

            try
            {
                var book = await _bookRepository.GetByIdAsync(command.Id);
                
                if (book == null)
                {
                    throw new Exception("No se encontró ese libro en la biblioteca");
                }

                book.Title = command.Title;
                book.AuthorId = command.AuthorId;
                await _bookRepository.UpdateAsync(book, book.Id);

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
