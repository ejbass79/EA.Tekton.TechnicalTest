using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Queries;

public class GetStateAllQuery(int page, int limit, string orderBy, bool ascending = true) : ICommand<QueryMultipleResponse<StateDto>>
{
    public int Page { get; } = page;
    public int Limit { get; } = limit;
    public string OrderBy { get; } = orderBy;
    public bool Ascending { get; } = ascending;
}