﻿using BackEnd.AppService.Models.Requests;
using FluentValidation;

namespace BackEnd.AppService.Domain.Validator;

public class SingInRequestValidator :  AbstractValidator<SignInRequest>
{
}