using System.Data;
using BackEnd.AppService.Extensions;
using BackEnd.AppService.Models.Requests;
using BackEnd.Repository;
using FluentValidation;

namespace BackEnd.AppService.Domain.Validator;

public class CreateNewPasswordValidator : AbstractValidator<CreateNewPasswordRequest>
{
    private readonly AppDbContext _appDbContext;
    public CreateNewPasswordValidator(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("E-mail is required")
            .EmailAddress().WithMessage("E-mail is invalid.");

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required");

        RuleFor(request => request.RememberNewPassword)
            .NotEmpty()
            .WithMessage("Remember new password is required");

        RuleFor(u => u.NewPassword)
            .Equal(x => x.RememberNewPassword, StringComparer.OrdinalIgnoreCase)
            .WithMessage("Password and password confirmation do not match.");


        RuleFor(e => e)
           .Custom((e, context) =>
           {
               if (!UserIsValid(e.Email, e.OldPassword))
               {
                   context.AddFailure("There is no employee with this email or Old-password.");
               }
           });
    }

    private bool UserIsValid(string email, string password)
    {
        var employee =
             _appDbContext
                    .Employee
                    .SingleOrDefault(e => e.Email == email);
        if (employee == null)
        {
            return false;
        }

        return JwtExtensions.VerifyPassword(password, employee.Password);
    }
}
