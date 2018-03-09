namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingIds : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JSONFiles");
            DropPrimaryKey("dbo.MainObjects");
            DropPrimaryKey("dbo.Registers");
            AlterColumn("dbo.JSONFiles", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.MainObjects", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Registers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.JSONFiles", "Id");
            AddPrimaryKey("dbo.MainObjects", "Id");
            AddPrimaryKey("dbo.Registers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Registers");
            DropPrimaryKey("dbo.MainObjects");
            DropPrimaryKey("dbo.JSONFiles");
            AlterColumn("dbo.Registers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.MainObjects", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.JSONFiles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Registers", "Id");
            AddPrimaryKey("dbo.MainObjects", "Id");
            AddPrimaryKey("dbo.JSONFiles", "Id");
        }
    }
}
