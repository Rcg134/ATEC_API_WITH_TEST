
using ATEC_API.Data.DTO.StagingDTO;

namespace ATEC_API.Data.IRepositories
{
    public interface IStagingRepository
    {
        Task<bool> IsTrackOut(StagingDTO stagingDTO);
    }
}