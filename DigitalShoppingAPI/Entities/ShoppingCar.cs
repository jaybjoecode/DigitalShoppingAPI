using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Entities
{
    public class ShoppingCar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
