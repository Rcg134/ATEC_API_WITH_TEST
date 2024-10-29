// <copyright file="DownloadCompressDTO.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.DTO.DownloadCompressDTO
{
    public class DownloadCompressDTO
    {
        public string? SP { get; set; }

        public string SheetName { get; set; }

        public string CacheKey { get; set; }

        public string ZipName { get; set; }
    }
}
