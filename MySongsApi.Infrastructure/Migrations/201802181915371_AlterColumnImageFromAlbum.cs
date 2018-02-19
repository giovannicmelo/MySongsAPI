namespace MySongsApi.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumnImageFromAlbum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.album", "image", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.album", "image", c => c.Binary());
        }
    }
}
