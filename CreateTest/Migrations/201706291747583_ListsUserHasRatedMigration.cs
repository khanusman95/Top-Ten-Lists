namespace CreateTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListsUserHasRatedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListUserHasRateds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        List_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TopTenLists", t => t.List_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.List_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListUserHasRateds", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ListUserHasRateds", "List_ID", "dbo.TopTenLists");
            DropIndex("dbo.ListUserHasRateds", new[] { "User_Id" });
            DropIndex("dbo.ListUserHasRateds", new[] { "List_ID" });
            DropTable("dbo.ListUserHasRateds");
        }
    }
}
