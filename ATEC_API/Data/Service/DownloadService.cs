// <copyright file="DownloadService.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Service
{
    using System.Data;
    using System.IO.Compression;
    using ATEC_API.Data.DTO.DownloadCompressDTO;
    using ATEC_API.Data.IRepositories;
    using Dapper;
    using Microsoft.Data.SqlClient;
    using OfficeOpenXml;

    public class DownloadService
    {
    private readonly IDapperConnection _dapperConnection;
    private readonly CacheManagerService _cacheManagerService;

    public DownloadService(IDapperConnection dapperConnection, CacheManagerService cacheManagerService)
    {
            this._dapperConnection = dapperConnection;
            this._cacheManagerService = cacheManagerService;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
    }

    public async Task<MemoryStream> DownloadToExcelAndCompress<Imodel>(DownloadCompressDTO downloadCompressDTO ,DynamicParameters dynamicParameters)
    {
        using (SqlConnection sqlConnection = this._dapperConnection.MES_ATEC_CreateConnection())
        {
            IEnumerable<Imodel> SPList = Enumerable.Empty<Imodel>();

            var useSP = downloadCompressDTO.CacheKey.ToString();
            var cacheValue = await this._cacheManagerService.GetListAsync<Imodel>(useSP);
            if (cacheValue == null)
            {
                SPList = await sqlConnection.QueryAsync<Imodel>(useSP, dynamicParameters, commandType: CommandType.StoredProcedure);
                await this._cacheManagerService.SetListAsync<Imodel>(useSP, SPList.ToList());
            }

            if (cacheValue != null)
            {
              SPList = cacheValue;
            }

            if (cacheValue?.Count == 0)
            {
              SPList = Enumerable.Empty<Imodel>();
            }

            // Create Excel file
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add(downloadCompressDTO.SheetName);
                var properties = typeof(Imodel).GetProperties();

                // Write headers dynamically
                for (var col = 0; col < properties.Length; col++)
                {
                    // Since Excel is 1-indexed, we start with 1
                    worksheet.Cells[1, col + 1].Value = properties[col].Name;

                    // Check if the property is of type DateTime or Nullable<DateTime> and apply formatting
                    if (properties[col].PropertyType == typeof(DateTime) || properties[col].PropertyType == typeof(DateTime?))
                    {
                        worksheet.Column(col + 1).Style.Numberformat.Format = "yyyy-mm-dd";
                    }
                }

                // Apply formatting to the header row
                for (var col = 1; col <= properties.Length; col++)
                {
                    worksheet.Cells[1, col].Style.Font.Bold = true;
                    worksheet.Cells[1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                // Populate data
                var row = 2;
                foreach (var list in SPList)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var cellValue = properties[col].GetValue(list);

                        if (cellValue is DateTime dateValue)
                        {
                             worksheet.Cells[row, col + 1].Value = cellValue;
                             worksheet.Cells[row, col + 1].Style.Numberformat.Format = "yyyy-mm-dd";
                        }
                        else
                        {
                            worksheet.Cells[row, col + 1].Value = cellValue;
                        }
                    }

                    row++;
                }

                // Save Excel file to stream
                MemoryStream memoryStream = new MemoryStream();
                excelPackage.SaveAs(memoryStream);

                // Compress the Excel file
                memoryStream.Position = 0;
                MemoryStream compressedStream = new MemoryStream();
                using (var archive = new ZipArchive(compressedStream, ZipArchiveMode.Create, true))
                {
                    var entry = archive.CreateEntry($"{downloadCompressDTO.SheetName}.xlsx");
                    using (var entryStream = entry.Open())
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        await memoryStream.CopyToAsync(entryStream);
                    }
                }

                // Reset the position of the compressed stream
                compressedStream.Position = 0;

                return compressedStream;
            }
        }
    }
   }
}
