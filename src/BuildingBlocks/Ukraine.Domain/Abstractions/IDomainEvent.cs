namespace Ukraine.Domain.Abstractions
{
    public interface IDomainEvent
    {
        DateTime CreatedAt { get; }
    }
}
