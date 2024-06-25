using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EA.Tekton.TechnicalTest.Domain.Interfaces;

public interface IProductService
{
    Task<ProductResponse> GetByProductIdAsync(int productId);

    Task<QueryMultipleResponse<ProductDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);
        
    Task<(bool status, int productId)> PostAsync(ProductDto entity);

    Task<bool> PutAsync(int productId, ProductDto entity);

    Task<bool> DeleteAsync(int productId);

    Task<IEnumerable<ProductDto>> GetAllByStatusIdAsync(int statusId);
        
    
}