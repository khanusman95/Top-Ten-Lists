namespace CreateTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemIdRemoval : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rateds", "ItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rateds", "ItemID", c => c.Int(nullable: false));
        }
    }
}
