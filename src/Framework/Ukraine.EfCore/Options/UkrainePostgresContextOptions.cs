namespace Ukraine.EfCore.Options;

public class UkrainePostgresContextOptions : UkraineContextOptions
{
	public string? MigrationsSchema { get; set; }
}