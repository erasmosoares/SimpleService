namespace SimpleService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedSampleFiles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                 INSERT INTO [dbo].[JSONFiles] ( [File]) VALUES ( N'{""name"":""john"", ""age"":22,""class"":""mca""}') 
                 INSERT INTO[dbo].[JSONFiles]( [File]) VALUES( N'{""name"":""david"", ""age"":22,""class"":""mca""}')
                 INSERT INTO[dbo].[JSONFiles]( [File]) VALUES( N'{""name"":""john"", ""age"":22,""class"":""mca"", ""surname"":""wall""}')
                 INSERT INTO[dbo].[JSONFiles] ([File]) VALUES( N'{""name"":""john"", ""age"":22,""class"":""mca"",""surname"":""smith""}')            
            ");
        }

    public override void Down()
        {
        }
    }
}
