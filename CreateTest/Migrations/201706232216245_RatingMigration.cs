namespace CreateTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListItems", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.ListItems", "TotalRaters", c => c.Int(nullable: false));
            DropColumn("dbo.ListItems", "Votes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ListItems", "Votes", c => c.String());
            DropColumn("dbo.ListItems", "TotalRaters");
            DropColumn("dbo.ListItems", "Rating");
        }
    }
}
