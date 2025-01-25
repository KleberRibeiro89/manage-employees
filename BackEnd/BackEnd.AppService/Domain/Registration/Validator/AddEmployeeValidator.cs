using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.Repository;
using FluentValidation;

namespace BackEnd.AppService.Domain.Registration.Validator;

public class AddEmployeeValidator : AbstractValidator<AddEmployeeRequest>
{
    private readonly AppDbContext _appDbContext;
    public AddEmployeeValidator(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("First Name is required");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Last name is required");

        RuleFor(e => e.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email is required");

        RuleFor(e => e.DocNumer)
            .NotEmpty()
            .NotNull()
            .WithMessage("Doc number is required");

        RuleFor(u => u.DocNumer)
           .Custom((DocNumer, context) =>
           {
               if (!_appDbContext.Employee.Any(u => u.DocNumer == DocNumer))
               {
                   context.AddFailure("existing document.");
               }
           });

        RuleFor(u => u.Email)
           .Custom((Email, context) =>
           {
               if (!_appDbContext.Employee.Any(u => u.Email == Email))
               {
                   context.AddFailure("existing email.");
               }
           });
    }
}
