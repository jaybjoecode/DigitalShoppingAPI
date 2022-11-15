using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class AddPhotoDTO
    {
        public int ProductId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
