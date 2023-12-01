using AutoMapper;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Dtos.Author;
using Library.Core.Application.Dtos.Book;
using Library.Core.Application.Features.Account.Commands.Authenticate;
using Library.Core.Application.Features.Account.Commands.RegisterUser;
using Library.Core.Application.Features.Author.Commands.AddAuthorCommand;
using Library.Core.Application.Features.Author.Commands.UpdateAuthorCommand;
using Library.Core.Application.Features.Book.Commands.AddBookCommand;
using Library.Core.Application.Features.Book.Commands.UpdateBookCommand;
using Library.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region Account

			CreateMap<AuthenticationRequest, AuthenticateCommand>()
				.ReverseMap();

            CreateMap<RegisterRequest, RegisterUserCommand>()
                .ForMember(x => x.Image, opt => opt.Ignore())
				.ReverseMap()
                .ForMember(x => x.UrlImage, opt => opt.Ignore());

            #endregion

            #region Book

            CreateMap<Book, AddBookCommand>()
                .ReverseMap()
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            
            CreateMap<Book, UpdateBookCommand>()
                .ReverseMap()
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Book, BookDTO>()
                .ReverseMap()
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion

            #region Author

            CreateMap<Author, AddAuthorCommand>()
                .ReverseMap()
                .ForMember(x => x.Books, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Author, UpdateAuthorCommand>()
                .ReverseMap()
                .ForMember(x => x.Books, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Author, AuthorDTO>()
                .ReverseMap()
                .ForMember(x => x.Books, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion
        }
    }
}
