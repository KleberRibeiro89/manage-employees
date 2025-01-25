using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Domain.Registration.Models.Responses;
using BackEnd.Repository.Entities;

namespace BackEnd.AppService.Domain.Registration;

public interface IRegistrationService
{
    Task CreateAsync(AddEmployeeRequest request);
    Task<EmployeeResponse> GetAsync(Guid id);
    Task<List<EmployeeResponse>> GetAsync(Predicate<Employee> predicate);
    Task<List<EmployeeResponse>> GetAsync();
    Task UpdateAsync(UpdateEmployeeRequest request);
    Task DeleteAsync(Guid id);
}
