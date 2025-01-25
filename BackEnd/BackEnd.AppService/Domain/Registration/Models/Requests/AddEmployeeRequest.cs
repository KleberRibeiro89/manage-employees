using BackEnd.Repository.Entities;

namespace BackEnd.AppService.Domain.Registration.Models.Requests;

public record AddEmployeeRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocNumer { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid ManagerId { get; set; }
    public bool NewPasswordRequired { get; set; }
    public Guid PositionEmployeeId { get; set; }


    public static implicit operator Employee (AddEmployeeRequest request)
    {
        return new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DocNumer = request.DocNumer,
            Password = request.Password,
            DateOfBirth = request.DateOfBirth,
            NewPasswordRequired = true,
            PositionEmployeeId = request.PositionEmployeeId,
        };
    }
}
