using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Models.DTOs
{
    public class NationalParkDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
