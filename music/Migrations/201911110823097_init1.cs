namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavouriteSongs",
                c => new
                {
                    FavouriteSongId = c.Int(nullable: false, identity: true),
                    user = c.String(),
                    SongId = c.Int(nullable: false),
                    SongName = c.String(),
                })
                .PrimaryKey(t => t.FavouriteSongId);

        }

        public override void Down()
        {
            DropTable("dbo.FavouriteSongs");
        }
    }
}
