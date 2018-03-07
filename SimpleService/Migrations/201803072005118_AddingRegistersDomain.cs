namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRegistersDomain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Left = c.String(),
                        Right = c.String(),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registers");
        }
    }
}
