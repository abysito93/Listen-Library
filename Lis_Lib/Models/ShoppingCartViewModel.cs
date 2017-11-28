using System.Collections.Generic;
using Lis_Lib.Models;

namespace Lis_Lib.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}