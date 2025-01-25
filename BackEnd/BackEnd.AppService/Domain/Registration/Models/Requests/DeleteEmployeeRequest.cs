namespace BackEnd.AppService.Domain.Registration.Models.Requests;

public record DeleteEmployeeRequest
{
    public Guid Id { get; set; }
}
