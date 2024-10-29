using System.ComponentModel.DataAnnotations;

namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MaterialStagingHistoryDTO
    {
        [Required]
        public int MaterialType { get; set; }
        
        public int CustomerCode { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        [Required]
        public int Mode { get; set; }
    }
}
