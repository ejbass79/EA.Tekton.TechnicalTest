using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Queries
{
    public record GetUserRolesQuery(string Email) : ICommand<List<string>>
    {
        public string Email { get; set; } = Email;
    }
}
