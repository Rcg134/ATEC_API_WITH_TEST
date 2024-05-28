using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.GeneralModels.MESATECModels.CantierResponse
{
    public class CantierResponse
    {
   
        public string LotNumber { get; set; }
        public string ProductID { get; set; }
        public string Device { get; set; }
        public string PackageID { get; set; }
        public string LeadTypeID { get; set; }
        public string Quantity { get; set; }
        public string StageID { get; set; }
        public string RecipeID { get; set; }
    }
}
