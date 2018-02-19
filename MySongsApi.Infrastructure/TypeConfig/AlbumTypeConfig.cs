using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Infrastructure.TypeConfig
{
    public class AlbumTypeConfig : AbstractTypeConfig<Album>
    {
        protected override void ConfigTableName()
        {
            ToTable("album");
        }

        protected override void ConfigTableFields()
        {
            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnName("id");

            Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(150);

            Property(p => p.Image)
                .IsOptional()
                .HasMaxLength(500)
                .HasColumnName("image");

            Property(p => p.ReleasedDate)
                .IsOptional()
                .HasColumnName("released_date")
                .HasColumnType("datetime2")
                .HasPrecision(0);

            Property(p => p.IdBand)
                .IsRequired()
                .HasColumnName("id_band");
        }

        protected override void ConfigPrimaryKey()
        {
            HasRequired(p => p.Band)
                .WithMany(p => p.Albums)
                .HasForeignKey(fk => fk.IdBand);
        }

        protected override void ConfigForeignKey()
        {
            HasRequired(p => p.Band)
                .WithMany(p => p.Albums)
                .HasForeignKey(fk => fk.IdBand);
        }
    }
}
