using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Configurations;

internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.HasKey(b => b.Id);
		builder.Property(b => b.FullName).IsRequired();
		builder.Property(b => b.SuperSecretKey).IsRequired();
	}
}