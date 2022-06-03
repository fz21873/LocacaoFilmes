using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System;
using System.Collections.Generic;
using System.Text;
namespace DAL.Mapping
{
    public class FilmMap : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            
            builder.ToTable("Film");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).HasColumnName("titulo").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasColumnType("varchar(500)").IsRequired();
            builder.Property(x => x.Tipo).HasColumnName("tipo").HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.DataLancamento).HasColumnName("data_lancamento").HasColumnType("date").IsRequired();
            builder.HasOne(x => x.Director)
                 .WithMany(x => x.Film)
                 .HasForeignKey(x=>x.DirectorId);
        }
    }
}
