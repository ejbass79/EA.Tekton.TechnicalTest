using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;
public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password, string Token) : ICommand
{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string Email { get; set; } = Email;
    public string Password { get; set; } = Password;
    public string Token { get; set; } = Token;
}
