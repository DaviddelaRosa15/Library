using AutoMapper;
using MediatR;
using Library.Core.Application.Dtos.Book;
using Library.Core.Application.Interfaces.Reposirories;

namespace Library.Core.Application.Features.Book.Queries.GetAllBook
{
    public class GetAllBookQuery : IRequest<List<BookDTO>>
    {

    }

    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, List<BookDTO>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDTO>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
        {
            List<BookDTO> result = new();

            try
            {
                var books = await _bookRepository.GetAllAsync();
                result = _mapper.Map<List<BookDTO>>(books);
            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
