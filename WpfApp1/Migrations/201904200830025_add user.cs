namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CRUD_Users", newName: "testUsers");
            AddColumn("dbo.testUsers", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.testUsers", "FirstName");
            DropColumn("dbo.testUsers", "LastName");
            DropColumn("dbo.testUsers", "Email");
            DropColumn("dbo.testUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.testUsers", "Password", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.testUsers", "Email", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.testUsers", "LastName", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.testUsers", "FirstName", c => c.String(nullable: false, maxLength: 75));
            DropColumn("dbo.testUsers", "Name");
            RenameTable(name: "dbo.testUsers", newName: "CRUD_Users");
        }
    }
}
