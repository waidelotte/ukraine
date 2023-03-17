using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Core;

public class SystemDateTimeProvider : IDateTimeProvider
{
	public DateTime NowUtc => DateTime.UtcNow;
}