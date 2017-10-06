using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public Group()
        {
            Categories = new List<Category>();
        }
    }

    public class CarouselNews
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Caption { get; set; }
        public string ImagePath { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}