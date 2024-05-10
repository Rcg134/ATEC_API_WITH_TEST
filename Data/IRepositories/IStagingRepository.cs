
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;

namespace ATEC_API.Data.IRepositories
{
    public interface IStagingRepository
    {
        Task<StagingResponse> IsTrackOut(StagingDTO stagingDTO);
    }
}