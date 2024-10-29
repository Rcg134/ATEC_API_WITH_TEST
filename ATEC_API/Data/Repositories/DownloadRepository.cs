// <copyright file="IDownloadRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Repositories
{
    using ATEC_API.Data.DTO.DownloadCompressDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.Data.Service;
    using Dapper;
    using Microsoft.AspNetCore.Mvc;

    public class DownloadRepository : IDownloadRepository
    {
        private readonly DownloadService _downloadService;

        public DownloadRepository(DownloadService downloadService)
        {
            this._downloadService = downloadService;
        }

        public async Task<MemoryStream> DownloadToExcelAndCompress<T>(DownloadCompressDTO downloadCompressDTO, DynamicParameters dynamicParameters)
        {
            var compressedExcelStream = await this._downloadService.DownloadToExcelAndCompress<T>(downloadCompressDTO , dynamicParameters);

            compressedExcelStream.Seek(0, SeekOrigin.Begin);

            return compressedExcelStream;
        }
    }
}
