using ATEC_API.Data.DTO.Cantier;
using ATEC_API.GeneralModels.MESATECModels.CantierResponse;
namespace ATEC_API.Data.IRepositories
{
    public interface ICantierRepository
    {
        Task<IEnumerable<CantierResponse>>? GetLotDetails(CantierDTO cantierDTO);
        Task<CantierResponse>? GetTrackInDetails(CantierDTO cantierDTO);
        Task<CantierResponse>? GetTrackOutDetails(CantierDTO cantierDTO);
        Task<IEnumerable<RecipeLoadResponse>>? RecipeLoadDetails(CantierDTO cantierDTO);
    }
}
