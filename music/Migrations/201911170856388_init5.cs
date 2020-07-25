namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FavouriteSongs", "SongId");
            AddForeignKey("dbo.FavouriteSongs", "SongId", "dbo.Songs", "SongId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteSongs", "SongId", "dbo.Songs");
            DropIndex("dbo.FavouriteSongs", new[] { "SongId" });
        }
    }
}
