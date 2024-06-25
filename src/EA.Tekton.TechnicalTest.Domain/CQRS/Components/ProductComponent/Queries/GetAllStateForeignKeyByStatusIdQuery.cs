using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;

public class GetAllStateForeignKeyByStatusIdQuery(int statusId) : ICommand<IEnumerable<ProductDto>>
{
    public int StatusId { get; set; } = statusId;
}