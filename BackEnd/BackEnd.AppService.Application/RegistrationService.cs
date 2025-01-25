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

    public Task CreateAsync(AddEmployeeRequest request)
    {
        request.Password = StringConstants.DefaultPassword;
        var validate = new AddEmployeeValidator(_appDbContext).Validate(request);
        if (!validate.IsValid)
        {
            throw new CustomValidatorException(validate.Errors);
        }

        Employee employee = request;
        _appDbContext.Employee.Add(employee);
        _appDbContext.SaveChanges();

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
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

    public Task<List<EmployeeResponse>> GetAsync(Predicate<Employee> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<List<EmployeeResponse>> GetAsync()
    {
        return _appDbContext
                    .Employee
                    .ToList()
                    .Select(e=> EmployeeResponse.ToResponse(e)).ToList();
    }

    public async Task UpdateAsync(UpdateEmployeeRequest request)
    {
        var validate = new UpdateEmployeeValidator().Validate(request);
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
        await _appDbContext.SaveChangesAsync();
    }
}
