using DigitalShoppingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ShoppingCarDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
    }
}
