namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Playlists", "PlaylistName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Playlists", "PlaylistName", c => c.String());
        }
    }
}
