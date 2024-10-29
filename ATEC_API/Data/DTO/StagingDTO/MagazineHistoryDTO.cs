// <copyright file="MagazineHistoryDTO.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MagazineHistoryDTO
    {
        public string Lot { get; set; }

        public string MagazineCode { get; set; }

        public int MagazineQty { get; set; }

        public string PackageId { get; set; }

        public string LeadCount { get; set; }

        public string StatusId { get; set; }

        public string StageId { get; set; }

        public DateTime? DateTime_TrackIn { get; set; }

        public DateTime? DateTime_TrackOut { get; set; }

        public string ScannedBy { get; set; }

        public string Remarks { get; set; }

        public DateTime Due_Date { get; set; }

        public string Color { get; set; }

        public string MachineId { get; set; }

        public string CustomerID { get; set; }
    }
}
