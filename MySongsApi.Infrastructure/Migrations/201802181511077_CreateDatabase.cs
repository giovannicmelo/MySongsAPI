namespace MySongsApi.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.album",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 150),
                        image = c.Binary(),
                        released_date = c.DateTime(precision: 0, storeType: "datetime2"),
                        id_band = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.band", t => t.id_band, cascadeDelete: true)
                .Index(t => t.id_band);
            
            CreateTable(
                "dbo.band",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 150),
                        genre = c.String(maxLength: 50),
                        bigraphy = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.song",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 150),
                        duration = c.Int(),
                        id_album = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.album", t => t.id_album, cascadeDelete: true)
                .Index(t => t.id_album);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.song", "id_album", "dbo.album");
            DropForeignKey("dbo.album", "id_band", "dbo.band");
            DropIndex("dbo.song", new[] { "id_album" });
            DropIndex("dbo.album", new[] { "id_band" });
            DropTable("dbo.song");
            DropTable("dbo.band");
            DropTable("dbo.album");
        }
    }
}
