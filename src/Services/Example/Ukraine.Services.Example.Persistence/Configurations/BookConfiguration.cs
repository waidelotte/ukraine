using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.HasKey(b => b.Id);
		builder.Property(o => o.Name).IsRequired();
		builder.Property(o => o.Rating).IsRequired();

		builder.HasOne(o => o.Author)
			.WithMany(m => m.Books)
			.HasForeignKey(k => k.AuthorId)
			.IsRequired();
	}
}