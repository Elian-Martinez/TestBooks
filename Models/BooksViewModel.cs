using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBooks.Models
{
    public class BooksViewModel
    {
        public Books Book { get; set; }
        public List<Books> Books { get; set; }

        public BooksViewModel()
        {
            Books = new List<Books>();
        }
    }
}
