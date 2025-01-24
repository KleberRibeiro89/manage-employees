using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository.Entities;

[Index(nameof(DocNumer), IsUnique = true)]
public record Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocNumer { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool NewPasswordRequired { get; set; }

    [ForeignKey("PositionEmployee")] 
    public Guid PositionEmployeeId { get; set; }
    public virtual PositionEmployee PositionEmployee { get; set; }
}
