using Library.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Domain.Entities
{
    public class Book : AuditableBaseEntity
    {
        public string Title { get; set; }

        //Navigation Properties
        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public Book()
        {
            this.Id = "";
        }
    }
}
