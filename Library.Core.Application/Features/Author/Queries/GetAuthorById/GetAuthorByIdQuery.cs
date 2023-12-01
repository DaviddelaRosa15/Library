using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Application.Dtos.Author;
using Library.Core.Application.Interfaces.Reposirories;

namespace Library.Core.Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<AuthorDTO>
    {
        public string Id { get; set; }
    }

    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            AuthorDTO response = new();

            try
            {
                var author = await _authorRepository.GetByIdAsync(query.Id);
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
