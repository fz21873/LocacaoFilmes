using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace DAL.Mapping
{
    public class DirectorMap : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.ToTable("Director");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.SobreNome).HasColumnName("sobre_nome").HasColumnType("varchar(20)").IsRequired();
            builder.HasMany(x => x.Film)
                .WithOne(x => x.Director);
              

          
               

         
               
        }
    }
}
