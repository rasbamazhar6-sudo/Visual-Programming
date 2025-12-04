using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_11.Models
{
        public class Book
        {
            public int BookId { get; set; }
            public string? Title { get; set; } 
            public int AuthorId { get; set; }
            public int CategoryId { get; set; }

            
            public Author? Author { get; set; }
            public Category? Category { get; set; }
        }
    

}
