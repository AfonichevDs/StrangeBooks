using System.Collections.Generic;

namespace BookStore.Models
{
    public class HomeIndexModel
    {
        public List<Book> LastBooks { get; set; }

        public List<Book> BestBooks { get; set; }

        public List<CarouselNews> Carousels { get; set; }
    }
}