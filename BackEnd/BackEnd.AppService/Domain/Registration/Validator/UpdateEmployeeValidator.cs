using BackEnd.AppService.Domain.Registration.Models.Requests;
using FluentValidation;

namespace BackEnd.AppService.Domain.Registration.Validator;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeValidator()
    {
        
    }
}
