using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Infrastructure.TypeConfig
{
    public class BandTypeConfig : AbstractTypeConfig<Band>
    {
        protected override void ConfigTableName()
        {
            ToTable("band");
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

            Property(p => p.Genre)
                .IsOptional()
                .HasColumnName("genre")
                .HasMaxLength(50);

            Property(p => p.Biography)
                .IsOptional()
                .HasColumnName("bigraphy")
                .HasMaxLength(500);
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigForeignKey()
        {
            
        }
    }
}
