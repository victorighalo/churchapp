namespace KingdomJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeddesignationpropertytomembersmodel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "designation_Id", "dbo.Designations");
            DropIndex("dbo.Members", new[] { "designation_Id" });
            RenameColumn(table: "dbo.Members", name: "designation_Id", newName: "DesignationId");
            AlterColumn("dbo.Members", "DesignationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "DesignationId");
            AddForeignKey("dbo.Members", "DesignationId", "dbo.Designations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "DesignationId", "dbo.Designations");
            DropIndex("dbo.Members", new[] { "DesignationId" });
            AlterColumn("dbo.Members", "DesignationId", c => c.Int());
            RenameColumn(table: "dbo.Members", name: "DesignationId", newName: "designation_Id");
            CreateIndex("dbo.Members", "designation_Id");
            AddForeignKey("dbo.Members", "designation_Id", "dbo.Designations", "Id");
        }
    }
}
