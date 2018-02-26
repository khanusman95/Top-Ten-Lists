namespace CreateTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasRatedMgration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rateds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        ListItem_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ListItems", t => t.ListItem_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ListItem_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rateds", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rateds", "ListItem_ID", "dbo.ListItems");
            DropIndex("dbo.Rateds", new[] { "User_Id" });
            DropIndex("dbo.Rateds", new[] { "ListItem_ID" });
            DropTable("dbo.Rateds");
        }
    }
}
