namespace BackEnd.AppService.Models.Requests;

public record ForgotPasswordRequest
{
    public string Email { get; set; }
}
