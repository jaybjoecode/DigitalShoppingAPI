using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ProfileInfoUpdateDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
