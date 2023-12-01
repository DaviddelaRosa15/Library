using AutoMapper;
using MediatR;
using Library.Core.Application.Dtos.Author;
using Library.Core.Application.Interfaces.Reposirories;

namespace Library.Core.Application.Features.Author.Queries.GetAllAuthor
{
    public class GetAllAuthorQuery : IRequest<List<AuthorDTO>>
    {

    }

    public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, List<AuthorDTO>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAllAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<List<AuthorDTO>> Handle(GetAllAuthorQuery query, CancellationToken cancellationToken)
        {
            List<AuthorDTO> result = new();

            try
            {
                var authors = await _authorRepository.GetAllAsync();
                result = _mapper.Map<List<AuthorDTO>>(authors);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
