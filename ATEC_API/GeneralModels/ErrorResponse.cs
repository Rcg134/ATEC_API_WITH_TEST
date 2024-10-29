using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.GeneralModels
{
    public class ErrorResponse
    {
        public string title { get; set; } = "One or more validation errors occurred.";
        public int status { get; set; }
        public Dictionary<string, string[]>? error { get; set; }
    }
}