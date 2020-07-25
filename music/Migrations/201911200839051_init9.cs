namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SongHistories",
                c => new
                    {
                        SongHistoryId = c.Int(nullable: false, identity: true),
                        user = c.String(),
                        songId = c.String(),
                    })
                .PrimaryKey(t => t.SongHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SongHistories");
        }
    }
}
