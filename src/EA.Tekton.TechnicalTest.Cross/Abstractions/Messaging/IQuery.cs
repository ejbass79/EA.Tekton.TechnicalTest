using MediatR;

namespace EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
