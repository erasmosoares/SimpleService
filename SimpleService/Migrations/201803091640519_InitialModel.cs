namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JSONFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Left = c.String(),
                        Right = c.String(),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registers");
            DropTable("dbo.JSONFiles");
        }
    }
}
