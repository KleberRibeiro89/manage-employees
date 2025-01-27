namespace BackEnd.AppService.Domain.Registration.Models.Responses;

public record PositionResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
