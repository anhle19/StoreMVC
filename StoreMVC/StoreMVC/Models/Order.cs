using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreMVC.Models
{
    public enum OrderStatus
    {
        Cancel = 0,
        Confirmed = 1,
        Pending = 2,
        Shipped = 3,
    }

    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }   // Id người đặt (liên kết AspNetUsers)
        public DateTime OrderDate { get; set; }

        // Thông tin người nhận
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total {  get; set; }
        public decimal TotalCost => Total + ShippingCost;

        // Trạng thái
        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}