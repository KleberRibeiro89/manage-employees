using BackEnd.AppService.Models.Requests;
using BackEnd.Repository;
using FluentValidation;

namespace BackEnd.AppService.Domain.Security.Validator;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    private readonly AppDbContext _appDbContext;
    public ForgotPasswordValidator(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("E-mail is required");

        RuleFor(u => u.Email)
           .Custom((email, context) =>
           {
               if (_appDbContext.Employee.Any(u => u.Email == email))
               {
                   context.AddFailure("There is no employee with this email.");
               }
           });
    }
}
