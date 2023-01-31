﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ukraine.Services.Example.Domain.Entities;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Configurations
{
    public class ProductMappingConfiguration : IEntityTypeConfiguration<ExampleEntity>
    {
        public void Configure(EntityTypeBuilder<ExampleEntity> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }
}