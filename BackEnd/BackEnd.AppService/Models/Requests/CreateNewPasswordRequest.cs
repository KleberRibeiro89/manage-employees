namespace BackEnd.AppService.Models.Requests;

public record CreateNewPasswordRequest
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RememberNewPassword { get; set; }

}
