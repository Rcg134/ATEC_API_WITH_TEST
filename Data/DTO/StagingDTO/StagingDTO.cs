using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class StagingDTO
    {
        
        [Required]
        public string? LotAlias { get; set; }

    }
}