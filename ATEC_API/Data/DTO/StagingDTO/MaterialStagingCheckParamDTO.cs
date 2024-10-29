namespace ATEC_API.Data.DTO.StagingDTO
{
    public class MaterialStagingCheckParamDTO
    {
        public string? LotNumber { get; set; }
        public string? Machine {  get; set; }
        public int CustomerCode { get; set; }
        public int Mode { get; set; }
        public string SID { get; set; }
        public string MaterialId { get; set; }
        public string Serial {  get; set; }

    }
}
