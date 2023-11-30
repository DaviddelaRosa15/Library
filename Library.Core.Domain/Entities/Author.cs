using Library.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Domain.Entities
{
    public class Author : AuditableBaseEntity
    {
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<Book> Books { get; set; }

        public Author()
        {
            this.Id = "";
        }
    }
}
