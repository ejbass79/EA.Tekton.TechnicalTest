using EA.Tekton.TechnicalTest.Cross.Auth.Extensions;
using EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Entities;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Auth.Models;
using EA.Tekton.TechnicalTest.Cross.Configuration;
using EA.Tekton.TechnicalTest.Cross.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Services;

public class AuthService(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IMediator mediator) : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly RoleManager<IdentityRole> _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<AuthResult> PasswordSignInAsync(string email, string password, bool lockoutOnFailure)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure).ConfigureAwait(false);

        return result.IsLockedOut
            ? AuthResult.Failure(new string[] { "Account locked, too many invalid login attempts." })
            : result.MapToResult();
    }

    public async Task<AuthResult> RegisterUserAsync(AuthUser authUser, string password)
    {
        var userExist = await _userManager.FindByEmailAsync(authUser.Email).ConfigureAwait(false);

        if (userExist is not null)
        {
            return new AuthResult(false, new[] { $"User '{userExist.Email}' was found." });
        }

        var user = new User
        {
            Email = authUser.Email,
            UserName = authUser.Email,
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, CrossConfiguration.DefaultRolUser)).ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, CrossConfiguration.DefaultRolUser).ConfigureAwait(false);
        }
        else
        {
            return new AuthResult(false, new[] { $"Failed to create user '{authUser.Email}'." });
        }

        return result.MapToResult();
    }

    public async Task<IList<string>?> GetUserRolesAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

        if (user is null)
        {
            throw new NotFoundException($"user '{email}' was not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        return roles;
    }

    public async Task<AuthUser> GetUserAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

        if (user == null)
        {
            throw new BadRequestException($"Email '{email}' does not exist.");
        }

        var authUser = MapToAuthUser(user);

        return authUser;
    }

    private AuthUser MapToAuthUser(User user)
    {
        return new AuthUser
        {
            Email = user.Email!
        };
    }

}