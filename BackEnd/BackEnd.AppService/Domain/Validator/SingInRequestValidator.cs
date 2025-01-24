using BackEnd.AppService.Extensions;
using BackEnd.AppService.Models.Requests;
using BackEnd.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.AppService.Domain.Validator;

public class SingInRequestValidator :  AbstractValidator<SignInRequest>
{
    private readonly AppDbContext _appDbContext;
    public SingInRequestValidator(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("E-mail is required");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required");


        RuleFor(e => e)
           .Custom((e, context) =>
           {
               if (!UserIsValid(e.Email, e.Password))
               {
                   context.AddFailure("There is no employee with this email or password.");
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
