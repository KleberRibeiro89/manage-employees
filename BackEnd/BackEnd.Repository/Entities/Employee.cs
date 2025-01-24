namespace BackEnd.Repository.Entities;

public record Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocNumer { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Guid PositionEmployeeId { get; set; }
    public virtual PositionEmployee PositionEmployee { get; set; }
}
