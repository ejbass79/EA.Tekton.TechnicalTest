using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

public class CreateStateCommand(StateDto entity) : ICommand<(bool status, int statusId)>
{
    public StateDto State { get; set; } = entity;
}