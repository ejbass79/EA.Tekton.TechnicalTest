using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

public class DeleteProductCommand(int productId) : ICommand<bool>
{
    public int ProductId { get; set; } = productId;
}