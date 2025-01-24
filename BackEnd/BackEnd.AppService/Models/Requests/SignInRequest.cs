namespace BackEnd.AppService.Models.Requests;

public record SignInRequest
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}
