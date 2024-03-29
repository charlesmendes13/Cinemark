﻿using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinemark.Infrastructure.Data.Mapping.SqlServer
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(c => c.Id).IsRequired();
            builder.Property(p => p.Nome).IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.Senha).IsRequired();
            builder.Property(p => p.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETDATE()");
        }
    }
}
