using System.ComponentModel.DataAnnotations;

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MaterialStaging
    {
        [Required]
        public string paramSid { get; set; }
        [Required]
        public string paramMaterialId { get; set; }
        [Required]
        public string paramSerial { get; set; }
        [Required]
        public string paramExpirationDate { get; set; }

        [Required]
        public string paramCustomerCode { get; set; }

        [Required]
        public string paramMaterialType { get; set; }
    }
}
