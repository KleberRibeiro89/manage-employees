﻿using BackEnd.AppService.Constants;
using BackEnd.AppService.Domain.Security;
using BackEnd.AppService.Domain.Security.Validator;
using BackEnd.AppService.Extensions;
using BackEnd.AppService.Models.Requests;
using BackEnd.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackEnd.AppService.Application;

public class SecurityService : ISecurityService
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;
    public SecurityService(
        AppDbContext appDbContext, 
        IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
    }

    public async Task CreateNewPasswordRequestAsync(CreateNewPasswordRequest request)
    {
        var validate = new CreateNewPasswordValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        var employee =
                await _appDbContext
                    .Employee
                    .Where(e => e.Email == request.Email)
                    .SingleAsync();

        employee.Password = JwtExtensions.HashPassword(request.NewPassword);
        employee.NewPasswordRequired = false;
        employee.Password = StringConstants.DefaultPassword;
        _appDbContext.Employee.Update(employee);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var validate = new ForgotPasswordValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        var employee =
            await _appDbContext
                    .Employee
                    .Where(e => e.Email == request.Email)
                    .SingleAsync();


        employee.Password = JwtExtensions.HashPassword(StringConstants.DefaultPassword);
        employee.NewPasswordRequired = true;
        _appDbContext.Employee.Update(employee);
        await _appDbContext.SaveChangesAsync();
    }

    public async ValueTask<string> SignInAsync(SignInRequest request)
    {
        var validate = new SingInRequestValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        var employee =
                await _appDbContext
                        .Employee
                        .Where(e => e.Email == request.Email)
                        .SingleAsync();

        return JwtExtensions.ToJwt(employee.Id, employee.Email, employee.PositionEmployee.Name, _configuration);
    }
}
