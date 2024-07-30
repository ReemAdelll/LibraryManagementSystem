using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Shared
{
    public class BorrowedBookBooksDTO : BorrowedBookDTO
    {
        public ICollection<BookDTO>? Books { get; set; }
    }
}
