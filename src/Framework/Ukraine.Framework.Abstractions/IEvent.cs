namespace Ukraine.Framework.Abstractions;

public interface IEvent { }

#pragma warning disable SA1402
public interface IEvent<out TId> : IEvent
#pragma warning restore SA1402
	where TId : struct
{
	TId EventId { get; }

	DateTime Created { get; }
}