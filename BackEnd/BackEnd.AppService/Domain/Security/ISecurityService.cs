using BackEnd.AppService.Models.Requests;

namespace BackEnd.AppService.Domain.Security;

public interface ISecurityService
{
    ValueTask<string> SignInAsync(SignInRequest request);
    Task ForgotPasswordAsync(ForgotPasswordRequest request);
    Task CreateNewPasswordRequestAsync(CreateNewPasswordRequest request);
}
