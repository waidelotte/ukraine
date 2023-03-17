using System.Diagnostics.CodeAnalysis;

namespace Ukraine.Framework.Abstractions;

public interface IAuditableEntity : IEntity
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
public interface IAuditableEntity<out TIdentity> : IEntity<TIdentity>, IAuditableEntity
	where TIdentity : struct { }