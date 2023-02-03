using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ukraine.Services.Example.Domain.Entities;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Configurations;

public class ExampleChildEntityConfiguration : IEntityTypeConfiguration<ExampleChildEntity>
{
    public void Configure(EntityTypeBuilder<ExampleChildEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(o => o.NotNullIntValue).IsRequired();
        
        builder.HasOne(o => o.ExampleEntity)
            .WithMany(m => m.ChildEntities)
            .HasForeignKey(k => k.ExampleEntityId)
            .IsRequired();
    }
}