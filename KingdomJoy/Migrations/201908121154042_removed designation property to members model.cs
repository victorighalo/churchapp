namespace KingdomJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeddesignationpropertytomembersmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "Designation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Designation", c => c.Byte(nullable: false));
        }
    }
}
