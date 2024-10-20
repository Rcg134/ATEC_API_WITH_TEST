// <copyright file="LoginResponse.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.GeneralModels.LoginModels
{
    public class LoginResponse
    {
        public int UserCode { get; set; }

        public string UserGroupID { get; set; }

        public string ApplicationID { get; set; }

        public string? FullName { get; set; }
    }
}
