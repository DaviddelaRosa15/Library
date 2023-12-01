using Library.Core.Application.Interfaces.Repositories;
using Library.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Application.Interfaces.Reposirories
{
    public interface IBookRepository : IGenericRepository<Book>
    {

    }
}
