namespace KingdomJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(),
                        Gender = c.Byte(nullable: false),
                        Status = c.Byte(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        Dom = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        BornAgain = c.Byte(nullable: false),
                        Baptized = c.Byte(nullable: false),
                        FoundationSchool = c.Byte(nullable: false),
                        Designation = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberTitles");
            DropTable("dbo.Members");
            DropTable("dbo.Genders");
            DropTable("dbo.Designations");
        }
    }
}
