namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SongHistories", "songId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SongHistories", "songId", c => c.String());
        }
    }
}
