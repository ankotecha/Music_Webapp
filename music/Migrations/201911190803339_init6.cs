namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Playlistid = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PlaylistName = c.String(),
                    })
                .PrimaryKey(t => t.Playlistid);
            
            CreateTable(
                "dbo.UserPlaylists",
                c => new
                    {
                        primaryid = c.Int(nullable: false, identity: true),
                        playlistid = c.Int(nullable: false),
                        songid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.primaryid);
            
            AddColumn("dbo.Songs", "posterpath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "posterpath");
            DropTable("dbo.UserPlaylists");
            DropTable("dbo.Playlists");
        }
    }
}
