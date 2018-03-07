namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingMainObjectsDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MainObjects (Id, Name, Date) VALUES (1, 'Left',06/03/2018)");
            Sql("INSERT INTO MainObjects (Id, Name, Date) VALUES (2, 'Right',06/03/2018)");
        }
        
        public override void Down()
        {
        }
    }
}
