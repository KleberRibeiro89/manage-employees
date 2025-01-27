using FluentValidation;

namespace BackEnd.AppService.Domain.Registration.Validator;

public class DeleteEmployeeValidator: AbstractValidator<Guid>
{
    public DeleteEmployeeValidator()
    {
        RuleFor(e => e)
            .NotEmpty()
            .WithMessage("id is required");


    }
}
