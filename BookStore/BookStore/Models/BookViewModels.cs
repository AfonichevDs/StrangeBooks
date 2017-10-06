using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public List<Book> SimilarBooks { get; set; }
    }

    public class CommentsViewModel
    {
        public List<Comment> Comments { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}