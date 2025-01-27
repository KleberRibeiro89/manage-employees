using BackEnd.AppService.Constants;
using BackEnd.AppService.Domain.Registration;
using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Domain.Registration.Models.Responses;
using BackEnd.AppService.Domain.Registration.Validator;
using BackEnd.AppService.Extensions;
using BackEnd.Repository;
using BackEnd.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.AppService.Application;

public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _appDbContext;
    public RegistrationService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(AddEmployeeRequest request)
    {
        request.Password = JwtExtensions.HashPassword(StringConstants.DefaultPassword);
        var validate = new AddEmployeeValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        Employee employee = request;
        _appDbContext.Employee.Add(employee);
        await _appDbContext.SaveChangesAsync();

        _appDbContext.PhoneEmployee.AddRange(
            request.Phones.Select(p => new PhoneEmployee
            {
                EmployeeId = employee.Id,
                PhoneNumber = p,
                Id = Guid.NewGuid()
            }).ToList());

        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var validate = new DeleteEmployeeValidator().Validate(id);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        var phones = _appDbContext.PhoneEmployee.Where(p => p.EmployeeId == id).ToList();
        _appDbContext.PhoneEmployee.RemoveRange(phones);
        await _appDbContext.SaveChangesAsync();

        var employee = await _appDbContext.Employee.FirstAsync(e=> e.Id == id);
        _appDbContext.Employee.Remove(employee);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<EmployeeResponse> GetAsync(Guid id)
    {
        var entity = await _appDbContext
                .Employee
                .FirstOrDefaultAsync(e => e.Id == id);

        if (entity is null)
            return null;

        return EmployeeResponse.ToResponse(entity);
    }

    public async Task<List<EmployeeResponse>> GetAsync()
    {
        return _appDbContext
                    .Employee
                    .ToList()
                    .Select(e=> EmployeeResponse.ToResponse(e)).ToList();
    }

    public async ValueTask<bool> NewPasswordAsync(Guid id)
    {
        var employee = await _appDbContext
                        .Employee
                        .FirstAsync(e => e.Id == id);

        return employee.NewPasswordRequired;
    }

    public async Task UpdateAsync(UpdateEmployeeRequest request)
    {
        var validate = new UpdateEmployeeValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        var employee = await _appDbContext.Employee.FirstAsync(e=> e.Id == request.Id);
        employee.DateOfBirth = request.DateOfBirth;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.ManagerId = request.ManagerId;
        employee.PositionEmployeeId = request.PositionEmployeeId;

        _appDbContext.Employee.Update(employee);
        _appDbContext.PhoneEmployee.RemoveRange(employee.PhoneEmployee);

        _appDbContext.PhoneEmployee.AddRange(request.Phones.Select(p => new PhoneEmployee
        {
            EmployeeId = p.Key,
            PhoneNumber = p.Value
        }));
        
        await _appDbContext.SaveChangesAsync();
    }

    public List<PositionResponse> GetPositions()
    {
        return _appDbContext
                        .PositionEmployee
                        .Select(p => new PositionResponse
                        {
                            Id = p.Id,
                            Name = p.Name,
                        }).ToList();
    }
}
