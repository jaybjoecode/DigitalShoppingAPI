using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ValorationCreateDTO
    {
        public int ProductId { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
