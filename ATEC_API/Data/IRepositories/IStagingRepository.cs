namespace ATEC_API.Data.IRepositories
{
    using ATEC_API.Data.DTO.StagingDTO;
    using ATEC_API.GeneralModels;
    using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
    using Dapper;

    public interface IStagingRepository
    {
        Task<StagingResponse> IsTrackOut(StagingDTO stagingDTO);

        Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetail(MaterialStagingDTO materialStagingDTO);

        Task<IEnumerable<MaterialCustomerResponse>>? GetMaterialCustomer(int paramMaterialType);

        Task<IEnumerable<MaterialStagingResponse>>? CheckLotNumber(MaterialStagingCheckParamDTO materialStaging);

        Task<IEnumerable<MaterialCustomerResponse>>? GetCustomerHistory(MaterialStagingHistoryDTO materialHistory);

        Task<IEnumerable<MaterialStagingHistoryResponse>>? GetMaterialHistory(MaterialStagingHistoryDTO materialHistory);

        Task<(IEnumerable<MagazineHistoryDTO>, PageResultsResponse pageResultsResponse)> MagazineListDapperPagination(MagazineHistoryInput magazineHistoryInput);
        
        //New Material Staging Flow
        Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetailNew(MaterialStagingNewDTO materialStagingNewDTO);
      
        Task<IEnumerable<MaterialStagingResponse>>? GetMaterialLotMachine(MaterialStagingLMDTO materialStagingLMDTO);

        //Material Staging Panjit and Gem
        Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetailPG(MaterialStagingNewDTO materialStagingNewDTO);

    }
}