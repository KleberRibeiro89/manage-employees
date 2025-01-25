using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Repository.Entities;

public record PhoneEmployee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string PhoneNumber { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
}
