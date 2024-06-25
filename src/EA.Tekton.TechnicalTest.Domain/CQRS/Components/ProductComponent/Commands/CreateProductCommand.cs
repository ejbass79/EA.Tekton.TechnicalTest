using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

public class CreateProductCommand(ProductDto entity) : ICommand<(bool status, int productId)>
{
    public ProductDto Product { get; set; } = entity;
}