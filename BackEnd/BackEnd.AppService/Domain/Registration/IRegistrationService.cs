using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Domain.Registration.Models.Responses;

namespace BackEnd.AppService.Domain.Registration;

public interface IRegistrationService
{
    Task CreateAsync(AddEmployeeRequest request);
    Task<EmployeeResponse> GetAsync(Guid id);
    Task<List<EmployeeResponse>> GetAsync();
    Task UpdateAsync(UpdateEmployeeRequest request);
    Task DeleteAsync(Guid id);
    ValueTask<bool> NewPasswordAsync(Guid id);
    List<PositionResponse> GetPositions();
}
