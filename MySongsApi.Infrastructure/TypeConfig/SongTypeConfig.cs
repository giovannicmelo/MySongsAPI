using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Infrastructure.TypeConfig
{
    public class SongTypeConfig : AbstractTypeConfig<Song>
    {

        protected override void ConfigTableName()
        {
            ToTable("song");
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

            Property(p => p.Duration)
                .IsOptional()
                .HasColumnName("duration");

            Property(p => p.IdAlbum)
                .HasColumnName("id_album")
                .IsRequired();
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigForeignKey()
        {
            HasRequired(p => p.Album)
                .WithMany(p => p.Songs)
                .HasForeignKey(fk => fk.IdAlbum);
        }
    }
}
