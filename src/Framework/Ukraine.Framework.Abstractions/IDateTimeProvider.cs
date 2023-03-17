namespace Ukraine.Framework.Abstractions;

public interface IDateTimeProvider
{
	DateTime NowUtc { get; }
}