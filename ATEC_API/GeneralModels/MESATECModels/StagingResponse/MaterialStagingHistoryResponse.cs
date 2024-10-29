namespace ATEC_API.GeneralModels.MESATECModels.StagingResponse
{
    public class MaterialStagingHistoryResponse
    {
        public string? MaterialDesc { get; set; }
        public string? BatchNumber { get; set; }
        public string? CustomerID { get; set; }
        public string? ThawIn {  get; set; }
        public string? ThawInBy { get; set; }
        public string? ThawOut { get; set; }
        public string? ThawOutBy { get; set; }
        public string? WorkLifeEnd { get; set; }
        public string? ExpirationDate { get; set; }
        public string? DateUsed { get; set; }
        public string? LotNumber { get; set; }
        public string? MachineNo { get; set; }

    }
}
