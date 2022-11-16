using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ValorationDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
