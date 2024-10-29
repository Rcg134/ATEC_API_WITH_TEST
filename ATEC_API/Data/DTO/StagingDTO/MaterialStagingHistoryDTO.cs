// <copyright file="MaterialStagingHistoryDTO.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.DTO.StagingDTO
{
    using System.ComponentModel.DataAnnotations;

    public class MaterialStagingHistoryDTO
    {
        [Required]
        public int MaterialType { get; set; }
      
        public int CustomerCode { get; set; }
      
        public DateTime? DateFrom { get; set; }
      
        public DateTime? DateTo { get; set; }

        public int CustomerCode { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [Required]
        public int Mode { get; set; }
    }
}
