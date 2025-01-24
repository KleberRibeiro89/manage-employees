using BackEnd.AppService.Models.Requests;

namespace BackEnd.AppService.Domain;

public interface ISecurityService
{
    public ValueTask<string> SignInAsync(SignInRequest request);
    public Task ForgotPasswordAsync(ForgotPasswordRequest request);

}
