using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.DTO.ElogSheetDTO
{
    public class ELogSheetDTO
    {
        [Required]

        public string? Lotnumber { get; set; }
    
    }
}
