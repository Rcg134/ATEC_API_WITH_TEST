// <copyright file="ICantierRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.IRepositories
{
    using ATEC_API.Data.DTO.Cantier;
    using ATEC_API.GeneralModels.MESATECModels.CantierResponse;

    public interface ICantierRepository
    {
        Task<IEnumerable<CantierResponse>>? GetLotDetails(CantierDTO cantierDTO);

        Task<CantierResponse>? GetTrackInDetails(CantierDTO cantierDTO);

        Task<CantierResponse>? GetTrackOutDetails(CantierDTO cantierDTO);

        Task<IEnumerable<RecipeLoadResponse>>? RecipeLoadDetails(CantierDTO cantierDTO);
    }
}
