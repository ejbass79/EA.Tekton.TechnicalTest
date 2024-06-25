using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Auth.Models;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;

public class LoginCommand(string email, string password) : ICommand<AuthToken>
{
    public string Email { get; } = email;
    public string Password { get; } = password;
}
