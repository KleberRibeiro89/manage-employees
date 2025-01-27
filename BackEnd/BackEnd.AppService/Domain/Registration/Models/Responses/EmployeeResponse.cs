using BackEnd.Repository.Entities;

namespace BackEnd.AppService.Domain.Registration.Models.Responses;

public record EmployeeResponse
{
    public Guid Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocNumer { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid PositionEmployeeId { get; set; }
    public string PositionEmployee { get; set; }
    public Guid ManagerId { get; set; }
    public string ManagerName { get; set; }



    public static EmployeeResponse ToResponse(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            DocNumer = employee.DocNumer,
            Password = employee.Password,
            DateOfBirth = employee.DateOfBirth,
            PositionEmployee = employee.PositionEmployee.Name,
            PositionEmployeeId = employee.PositionEmployeeId,
            ManagerId = employee.ManagerId
        };
    }
}
