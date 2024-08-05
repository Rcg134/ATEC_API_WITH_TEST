
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;

namespace ATEC_API.Data.IRepositories
{
    public interface IStagingRepository
    {
        Task<StagingResponse> IsTrackOut(StagingDTO stagingDTO);
        Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetail(MaterialStagingDTO materialStagingDTO);
        Task<IEnumerable<MaterialCustomerResponse>>? GetMaterialCustomer(int paramMaterialType);
        Task<IEnumerable<MaterialStagingResponse>>? CheckLotNumber(MaterialStagingCheckParamDTO materialStaging);
    }
}