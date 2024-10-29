// <copyright file="MagazineHistoryInput.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MagazineHistoryInput
    {
        public string searchValue { get; set; } = string.Empty;

        public int stageValue { get; set; } = 0;

        public int customerCode { get; set; } = 0;

        public DateTime? dateFrom { get; set; }

        public DateTime? dateTo { get; set; }

        public int currentPage { get; set; } = 0;

        public int pageSize { get; set; } = 0;
    }
}
