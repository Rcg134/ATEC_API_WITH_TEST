// <copyright file="IDownloadRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.IRepositories
{
    using ATEC_API.Data.DTO.DownloadCompressDTO;
    using Dapper;

    public interface IDownloadRepository
    {
        Task<MemoryStream> DownloadToExcelAndCompress<T>(DownloadCompressDTO downloadCompressDTO,DynamicParameters dynamicParameters);
    }
}
