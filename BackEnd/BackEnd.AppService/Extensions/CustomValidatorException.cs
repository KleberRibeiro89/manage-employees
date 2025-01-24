using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace BackEnd.AppService.Extensions;

public class CustomValidatorException : Exception
{
    public readonly List<string> Errors;
    public CustomValidatorException(List<ValidationFailure>? errors) : base("Ocorreram erros de validação.")
    {
        Errors = errors.Select(e => $"{e.ErrorCode} - {e.ErrorMessage}").ToList();
    }
}
