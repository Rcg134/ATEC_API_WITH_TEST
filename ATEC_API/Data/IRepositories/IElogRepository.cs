namespace ATEC_API.Data.IRepositories
{
    using ATEC_API.Data.DTO.ElogSheetDTO;
    using ATEC_API.GeneralModels.MESATECModels.ElogResponse;
    public interface IElogRepository
    {
        Task<ElogResponse>? GetLotDetails(ELogSheetDTO eLogSheetDTO);
    }
}
