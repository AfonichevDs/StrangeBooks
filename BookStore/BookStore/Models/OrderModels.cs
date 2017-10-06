using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int? BookId { get; set; }
        public virtual Book Book { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        public decimal GetPrice()
        {
            return Book.Price * Quantity;
        }
    }

    public class Order
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Имя и фамилия")]
        public string FullName { get; set; }
        [Required]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Контактный телефон")]
        [RegularExpression(@"0[0-9]{2}-?[0-9]{3}-?[0-9]{2}-?[0-9]{2}", ErrorMessage = "Неправильный номер телефона")]
        public string Phone { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}