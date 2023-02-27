using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public sealed class AuditEntitiesSaveInterceptor : SaveChangesInterceptor
{
	public override InterceptionResult<int> SavingChanges(
		DbContextEventData eventData,
		InterceptionResult<int> result)
	{
		AuditEntities(eventData.Context!);

		return base.SavingChanges(eventData, result);
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		AuditEntities(eventData.Context!);

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private void AuditEntities(DbContext context)
	{
		foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedUtc = DateTime.UtcNow;
			}

			if (entry.State is EntityState.Added or EntityState.Modified)
			{
				entry.Entity.LastModifiedUtc = DateTime.UtcNow;
			}
		}
	}
}