using System.Collections.Generic;

namespace BookStore.Models
{
    public class GroupViewModel
    {
        public Group Group { get; set; }

        public List<Book> Books { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}