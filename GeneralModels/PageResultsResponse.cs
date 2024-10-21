// <copyright file="PageResultsResponse.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.GeneralModels
{
    public class PageResultsResponse
    {
        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int CurrentStart { get; set; }
    }
}
