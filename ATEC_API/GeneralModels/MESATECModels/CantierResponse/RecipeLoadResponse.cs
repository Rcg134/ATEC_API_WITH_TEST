using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATEC_API.GeneralModels.MESATECModels.CantierResponse
{
    public class RecipeLoadResponse
    {
        public string? EquipmentId { get; set; }
        public string? LotNumber { get; set; }
        public string? ProductId { get; set; }
        public string? Device { get; set; }
        public string? PackageId { get; set; }
        public string? LeadTypeId { get; set; }
        public string? Quantity { get; set; }
        public string? StageId { get; set; }
        public string? RecipeId { get; set; }
        
    }
}
