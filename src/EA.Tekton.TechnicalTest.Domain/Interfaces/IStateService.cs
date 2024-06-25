using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EA.Tekton.TechnicalTest.Domain.Interfaces;

public interface IStateService
{
    Task<StateDto> GetByStatusIdAsync(int statusId);

    Task<QueryMultipleResponse<StateDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);
        
    Task<(bool status, int statusId)> PostAsync(StateDto entity);

    Task<bool> PutAsync(int statusId, StateDto entity);

    Task<bool> DeleteAsync(int statusId);

    
        
    
}