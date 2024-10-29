using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATEC_API.Data.Context;
using ATEC_API.Data.DTO.HRISDTO;
using ATEC_API.Data.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace ATEC_API.Data.Repositories
{
    public class HRISRepository : IHRISRepository
    {
        private readonly HrisContext _hrisContext;

        public HRISRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task<bool> IsOperatorQualified(HRISDTO hrisDTO)

        public async Task<bool> IsOperatorQualified(HRISDTO hrisDTO ,CancellationToken cancellationToken)

        {
            var dateNow = DateTime.Now;
            var IsQualified = await _hrisContext
                                        .TblCerts.
                                        AnyAsync(isQual => isQual.EmpNo == hrisDTO.EmpNo &&
                                                 isQual.CustomerId == hrisDTO.CustomerId &&
                                                 isQual.RecipeCode == hrisDTO.RecipeCode &&
                                                 isQual.CertRequalDate >= dateNow);
                                                 isQual.CertRequalDate >= dateNow, cancellationToken);
            return IsQualified;
        }

    }
}