using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ProductCreateDTO
    {
        public IFormFile Cover { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> ProductPhotos { get; set; }
    }
}
