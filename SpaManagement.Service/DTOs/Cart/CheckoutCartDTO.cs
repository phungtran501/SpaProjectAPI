using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service.DTOs.Cart
{
    public class CheckoutCartDTO
    {
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string? Note { get; set; }
        public DateTime AppointmentDate { get; set; }

        public List<CartItemModel> Items { get; set; }

    }

}
