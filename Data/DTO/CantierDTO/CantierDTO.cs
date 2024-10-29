using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.DTO.Cantier
{
    public class CantierDTO
    {
        [Required]
        public string? LotNumber { get; set; }
    }
}
