using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Models
{
    [Table("AppUsers")]
    public class AppUser:BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

        [NotMapped]
        public string Token { get; set; }

    }
}
