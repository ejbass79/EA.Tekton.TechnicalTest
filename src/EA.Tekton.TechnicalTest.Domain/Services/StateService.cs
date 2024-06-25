using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.UnitOfWork;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EA.Tekton.TechnicalTest.Cross.MemoryCache.Services;
using static EA.Tekton.TechnicalTest.Cross.Enums.CrossEnum;

namespace EA.Tekton.TechnicalTest.Domain.Services;

public class StateService(IUnitOfWork unitOfWork, CacheService cacheService) : IStateService
{
    private readonly IRepository<State> _repository = unitOfWork.CreateRepository<State>();
    private readonly IUnitOfWork _unitOfWord = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        
    public async Task<StateDto> GetByStatusIdAsync(int statusId)
    {
        var resultCache = cacheService.GetFromCacheByKey<State>(statusId.ToString());

        if (resultCache == null)
        {
            var result = await _repository.FirstOrDefaultAsync(x => x.StatusId == statusId);

            cacheService.AddToCache(statusId.ToString(), result);

            return result.Adapt<StateDto>();
        }

        return resultCache.Adapt<StateDto>();
    }
        
    public async Task<QueryMultipleResponse<StateDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await _repository.GetAllAsync(page, limit, orderBy, ascending);

        return result.Adapt<QueryMultipleResponse<StateDto>>();
    }
        
    public async Task<(bool status, int statusId)> PostAsync(StateDto entity)
    {
        var obj = entity.Adapt<State>();

        _repository.Insert(obj);
        
        var result = await _unitOfWord.SaveAsync();

        return (result, obj.StatusId);
    }
        
    public async Task<bool> PutAsync(int statusId, StateDto entity)
    {
        var existingEntity = await _repository.FirstOrDefaultAsync(x => x.StatusId == statusId);
        
        if (existingEntity is null) 
            return false;

        existingEntity = entity.Adapt<State>();

        _repository.Update(existingEntity);
        
        return await _unitOfWord.SaveAsync();
    }
        
    public async Task<bool> DeleteAsync(int statusId)
    {
        var existingEntity = await _repository.FirstOrDefaultAsync(x => x.StatusId == statusId);
        
        if (existingEntity is null) 
            return false;

        _repository.Delete(existingEntity);
        
        return await _unitOfWord.SaveAsync();
    }

    
        
    
}