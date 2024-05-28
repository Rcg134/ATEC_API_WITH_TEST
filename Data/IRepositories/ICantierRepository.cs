using ATEC_API.Data.DTO.Cantier;
using ATEC_API.GeneralModels.MESATECModels.CantierResponse;
namespace ATEC_API.Data.IRepositories
{
    public interface ICantierRepository
    {
        Task<CantierResponse> GetLotDetails(CantierDTO cantierDTO);
    }
}
