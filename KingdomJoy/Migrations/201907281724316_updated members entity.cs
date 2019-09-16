namespace KingdomJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmembersentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "TitleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "TitleID");
            AddForeignKey("dbo.Members", "TitleID", "dbo.MemberTitles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "TitleID", "dbo.MemberTitles");
            DropIndex("dbo.Members", new[] { "TitleID" });
            DropColumn("dbo.Members", "TitleID");
        }
    }
}
