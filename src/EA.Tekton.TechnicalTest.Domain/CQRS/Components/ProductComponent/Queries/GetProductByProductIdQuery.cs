using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;

public class GetProductByProductIdQuery(int productId) : ICommand<ProductResponse>
{
    public int ProductId { get; set; } = productId;
}