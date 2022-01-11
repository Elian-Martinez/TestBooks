using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBooks.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int Description { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
