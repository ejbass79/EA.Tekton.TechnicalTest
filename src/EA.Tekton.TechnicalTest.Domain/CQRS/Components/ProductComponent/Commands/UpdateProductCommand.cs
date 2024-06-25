using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

public class UpdateProductCommand(int productId, ProductDto entity) : ICommand<bool>
{
    public int ProductId { get; set; } = productId;
    public ProductDto Product { get; set; } = entity;
}