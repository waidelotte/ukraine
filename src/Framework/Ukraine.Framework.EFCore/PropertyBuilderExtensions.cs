using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ukraine.Framework.EFCore;

public static class PropertyBuilderExtensions
{
	public static PropertyBuilder HasDefaultPostgresUuid(
		this PropertyBuilder propertyBuilder) =>
		propertyBuilder.HasDefaultValueSql("uuid_generate_v4()");

	public static PropertyBuilder HasDefaultPostgresDate(
		this PropertyBuilder propertyBuilder) =>
		propertyBuilder.HasDefaultValueSql("now()");

	public static PropertyBuilder HasUuidColumnType(
		this PropertyBuilder propertyBuilder) =>
		propertyBuilder.HasColumnType("uuid");
}