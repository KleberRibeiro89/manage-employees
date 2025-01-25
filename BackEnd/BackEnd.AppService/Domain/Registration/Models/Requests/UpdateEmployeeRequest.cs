using BackEnd.Repository.Entities;

namespace BackEnd.AppService.Domain.Registration.Models.Requests;

public record UpdateEmployeeRequest
{
    public Guid Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public Guid ManagerId { get; set; }
    public Guid PositionEmployeeId { get; set; }
}
