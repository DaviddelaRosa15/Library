using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Application.Dtos.Book;
using Library.Core.Application.Interfaces.Reposirories;

namespace Library.Core.Application.Features.Book.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<BookDTO>
    {
        public string Id { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            BookDTO response = new();

            try
            {
                List<BookDTO> resultDTO = new();
                var Book = await _bookRepository.GetByIdAsync(query.Id);
                response = _mapper.Map<BookDTO>(Book);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return response;
        }

    }
}
