namespace KingdomJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddesignationnavpropertytomembersmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "designation_Id", c => c.Int());
            CreateIndex("dbo.Members", "designation_Id");
            AddForeignKey("dbo.Members", "designation_Id", "dbo.Designations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "designation_Id", "dbo.Designations");
            DropIndex("dbo.Members", new[] { "designation_Id" });
            DropColumn("dbo.Members", "designation_Id");
        }
    }
}
