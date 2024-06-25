using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Queries;

public class GetStateByStatusIdQuery(int statusId) : ICommand<StateDto>
{
    public int StatusId { get; set; } = statusId;
}