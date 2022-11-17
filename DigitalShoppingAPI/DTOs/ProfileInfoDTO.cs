using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.DTOs
{
    public class ProfileInfoDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
