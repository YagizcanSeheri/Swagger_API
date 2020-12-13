using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Models
{
    [Table("NationalParks")]
    public class NationalPark:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public string CloudName { get; set; }

    }
}
