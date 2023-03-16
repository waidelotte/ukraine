using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ukraine.Framework.EFCore;

internal static class DateTimeConverter
{
	internal static readonly ValueConverter<DateTime, DateTime> ToUtcConverter =
		new(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
}