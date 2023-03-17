using System.Diagnostics.CodeAnalysis;

namespace Ukraine.Framework.Abstractions;

public interface IEntity { }

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
public interface IEntity<out TIdentity> : IEntity
	where TIdentity : struct
{
	public TIdentity Id { get; }
}