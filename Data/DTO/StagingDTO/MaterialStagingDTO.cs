using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MaterialStagingDTO
    {
        [Required]
        public string Sid { get; set; }
        [Required]
        public string MaterialId { get; set; }
        [Required]
        public string Serial { get; set; }
        [Required]
        public string ExpirationDate { get; set; }

        [Required]
        public string CustomerCode { get; set; }

        [Required]
        public string MaterialType { get; set; }
    }
}
