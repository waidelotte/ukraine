namespace Ukraine.Infrastructure.EfCore.Options;

public class UkrainePostgresOptions
{
    public int RetryOnFailureCount { get; set; } = 3;
    public TimeSpan RetryOnFailureDelay { get; set; } = TimeSpan.FromSeconds(5);
}