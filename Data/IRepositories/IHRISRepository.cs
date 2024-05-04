using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATEC_API.Data.DTO;
using ATEC_API.GeneralModels.HRISModels;

namespace ATEC_API.Data.IRepositories
{
    public interface IHRISRepository
    {
        Task<bool> IsOperatorQualified(HRISDTO hRISDTO);
    }
}