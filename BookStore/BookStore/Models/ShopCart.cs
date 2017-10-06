using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ShopCart
    {
        private List<OrderItem> OrderItems = new List<OrderItem>();

        public void AddItem(Book book, int quantity)
        {
            OrderItem OItem = OrderItems.Where(i => i.BookId == book.Id).FirstOrDefault();

            if(OItem == null)
            {
                OrderItems.Add(new OrderItem()
                {
                    BookId = book.Id,
                    Book = book,
                    Quantity = quantity 
                });
            }
            else
            {
                OItem.Quantity += quantity;
            }
        }

        public void ClearCart()
        {
            OrderItems.Clear();
        }

        public void RemoveItem(Book book)
        {
            var order = OrderItems.Find(o => o.BookId == book.Id);
            if (order.Quantity == 1)
            {
                OrderItems.Remove(order);
            } 
            else
            {
                order.Quantity--;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return OrderItems.Sum(b => b.Book.Price * b.Quantity);
            }
        }

        public int Quantity
        {
            get { return OrderItems.Count; }
        }

        public int BookQuantity()
        {
            return OrderItems.Sum(b => b.Quantity); 
        }

        public List<OrderItem> Items { get { return OrderItems; } }
    }
}