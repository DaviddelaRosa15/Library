using Library.Core.Application.Interfaces.Reposirories;
using Library.Core.Domain.Entities;
using Library.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationContext _dbContext;

        public BookRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
