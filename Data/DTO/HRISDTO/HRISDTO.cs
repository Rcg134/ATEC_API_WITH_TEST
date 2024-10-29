using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.Data.DTO.HRISDTO
{
    public class HRISDTO
    {

        [Required]
        public string? EmpNo { get; set; }
        [Required]
        public string? CustomerId { get; set; }
        [Required]
        public int RecipeCode { get; set; }

    }
}