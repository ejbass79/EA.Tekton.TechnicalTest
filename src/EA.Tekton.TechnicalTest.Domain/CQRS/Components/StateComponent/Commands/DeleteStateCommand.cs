using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

public class DeleteStateCommand(int statusId) : ICommand<bool>
{
    public int StatusId { get; set; } = statusId;
}