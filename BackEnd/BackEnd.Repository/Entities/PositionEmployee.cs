namespace BackEnd.Repository.Entities;

public record PositionEmployee
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
