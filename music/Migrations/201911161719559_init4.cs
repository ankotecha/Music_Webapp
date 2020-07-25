namespace music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "singer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "singer", c => c.String());
        }
    }
}
