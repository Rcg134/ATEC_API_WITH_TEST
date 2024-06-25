using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.GeneralModels.MESATECModels.CantierResponse
{
    public class CantierResponse
    {
        public bool? isSuccessful { get; set; }
        public string LotNumber { get; set; } = string.Empty;
        public string ProductID { get; set; }= string.Empty;
        public string Device { get; set; }= string.Empty;
        public string PackageID { get; set; }= string.Empty;
        public string LeadTypeID { get; set; }= string.Empty;
        public string QuantityIn { get; set; } = string.Empty;
        public string RejectQty {  get; set; }= string.Empty;
        public string QuantityOut { get; set; }= string.Empty;
        public string EquipmentID {  get; set; }= string.Empty;
        public string StageID { get; set; }= string.Empty;
        public string RecipeID { get; set; }= string.Empty;
        public string StatusID { get; set; }= string.Empty;
    }
}
