using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Enums;
using BackEnd.AppService.Extensions;
using BackEnd.Repository;
using FluentValidation;

namespace BackEnd.AppService.Domain.Registration.Validator;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeRequest>
{
    private readonly AppDbContext _appDbContext;
    public UpdateEmployeeValidator(AppDbContext appDbContext)
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


        RuleFor(e => e.DateOfBirth)
            .NotEmpty().WithMessage("ADate of birth is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("The employee must be at least 18 years old.");

        RuleFor(e => e)
           .Custom((e, context) =>
           {
               if (!HigherPermissions(e.ManagerId, e.PositionEmployeeId))
               {
                   context.AddFailure("your position does not allow you to update an employee of that position.");
               }
           });
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now.AddYears(-18);
    }

    private bool HigherPermissions(Guid managerId, Guid positionEmployeeId)
    {
        var manager = _appDbContext.Employee.First(e => e.Id == managerId);
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
