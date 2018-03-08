namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialFilesDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JSONFiles",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JSONFiles");
        }
    }
}
