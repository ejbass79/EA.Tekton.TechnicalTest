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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EA.Tekton.TechnicalTest.Domain.Services;

public class ProductService(IUnitOfWork unitOfWork, IStateService stateService, IConfiguration configuration) : IProductService
{
    private readonly IRepository<Product> _repository = unitOfWork.CreateRepository<Product>();
    private readonly IUnitOfWork _unitOfWord = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private static readonly HttpClient _client = new HttpClient();

    public async Task<ProductResponse> GetByProductIdAsync(int productId)
    {
        var result = await _repository.FirstOrDefaultAsync(x => x.ProductId == productId);

        if (result == null)
            throw new ArgumentException("No existe el producto");

        var response = result.Adapt<ProductResponse>();
       

        //Consultar en la cache sino va a la bases de datos
        var cacheState = await stateService.GetByStatusIdAsync(result.StatusId).ConfigureAwait(false);

        response.StatusName = cacheState!.StatusName;
            
        // HttpClient
        var resultProductMock = await GetMockApi().ConfigureAwait(false);
        if (resultProductMock.Any())
        {
            var product = resultProductMock.FirstOrDefault(apiResponse => apiResponse.Id == response.ProductId.ToString());
            
            response.Discount = product?.Percent ?? 0;

            response.FinalPrice = ((response.Price ?? 0.0m) * (100 - response.Discount) / 100);
        }
        else
        {
            throw new ArgumentException("No se pudo conectar a la MOCKAPI de descuentos");
        }
        
        return response;
    }
        
    public async Task<QueryMultipleResponse<ProductDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await _repository.GetAllAsync(page, limit, orderBy, ascending);

        return result.Adapt<QueryMultipleResponse<ProductDto>>();
    }
        
    public async Task<(bool status, int productId)> PostAsync(ProductDto entity)
    {
        var obj = entity.Adapt<Product>();

        _repository.Insert(obj);
        
        var result = await _unitOfWord.SaveAsync();

        return (result, obj.ProductId);
    }
        
    public async Task<bool> PutAsync(int productId, ProductDto entity)
    {
        var existingEntity = await _repository.FirstOrDefaultAsync(x => x.ProductId == productId);
        
        if (existingEntity is null) 
            return false;

        existingEntity = entity.Adapt<Product>();

        _repository.Update(existingEntity);
        
        return await _unitOfWord.SaveAsync();
    }
        
    public async Task<bool> DeleteAsync(int productId)
    {
        var existingEntity = await _repository.FirstOrDefaultAsync(x => x.ProductId == productId);
        
        if (existingEntity is null) 
            return false;

        _repository.Delete(existingEntity);
        
        return await _unitOfWord.SaveAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAllByStatusIdAsync(int statusId)
    {
        var result = await _repository.GetAllAsync(x => x.StatusId == statusId);
        return result.Adapt<IEnumerable<ProductDto>>();
    }

    private async Task<IList<MockApiResponse>>  GetMockApi()
    {
        var proxyMockApi = configuration.GetSection(nameof(ProxyMockApi)).Get<ProxyMockApi>();

        var responseMockApi = await GetRequest(proxyMockApi!.Url).ConfigureAwait(false);

        return responseMockApi;
    }

    private async Task<IList<MockApiResponse>> GetRequest(string url)
    {
        var response = await _client.GetAsync(url);
        
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<IList<MockApiResponse>>(result)!;
    }
}