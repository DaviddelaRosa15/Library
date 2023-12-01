using AutoMapper;
using Library.Core.Application.Dtos.Account;
using Library.Core.Application.Features.Account.Commands.Authenticate;
using Library.Core.Application.Features.Book.Commands.AddBookCommand;
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

            #endregion

            #region Book

            CreateMap<Book, AddAuthorCommand>()
                .ReverseMap()
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            #endregion
        }
    }
}
