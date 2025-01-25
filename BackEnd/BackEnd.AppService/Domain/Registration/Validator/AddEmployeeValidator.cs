using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Enums;
using BackEnd.AppService.Extensions;
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


        RuleFor(e => e.DateOfBirth)
            .NotEmpty().WithMessage("ADate of birth is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("The employee must be at least 18 years old.");


        RuleFor(u => u.DocNumer)
           .Custom((DocNumer, context) =>
           {
               if (_appDbContext.Employee.Any(u => u.DocNumer == DocNumer))
               {
                   context.AddFailure("existing document.");
               }
           });

        RuleFor(u => u.Email)
           .Custom((Email, context) =>
           {
               if (_appDbContext.Employee.Any(u => u.Email == Email))
               {
                   context.AddFailure("existing email.");
               }
           });

        RuleFor(e => e)
           .Custom((e, context) =>
           {
               if (!HigherPermissions(e.ManagerId, e.PositionEmployeeId))
               {
                   context.AddFailure("your position does not allow you to create an employee of that position.");
               }
           });
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now.AddYears(-18);
    }

    private bool HigherPermissions(Guid managerId, Guid positionEmployeeId)
    {
        var manager = _appDbContext.Employee.First(e=> e.Id == managerId);
        var employeePosition = EnumExtensions.GetPositionFromValue(positionEmployeeId);
        var managerPosition = EnumExtensions.GetPositionFromValue(manager.PositionEmployeeId);

        switch (managerPosition)
        {
            case PositionEnum.Director:
                return true;
            case PositionEnum.Leader:
                return employeePosition != PositionEnum.Director;
            case PositionEnum.Employee:
                return employeePosition == PositionEnum.Employee;
        }
        return false;
    }
}
