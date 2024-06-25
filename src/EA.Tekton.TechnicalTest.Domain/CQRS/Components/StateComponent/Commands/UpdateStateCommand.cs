using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

public class UpdateStateCommand(int statusId, StateDto entity) : ICommand<bool>
{
    public int StatusId { get; set; } = statusId;
    public StateDto State { get; set; } = entity;
}