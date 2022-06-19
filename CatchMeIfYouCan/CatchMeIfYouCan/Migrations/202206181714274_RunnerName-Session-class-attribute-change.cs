namespace CatchMeIfYouCan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RunnerNameSessionclassattributechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "RunnerName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "RunnerName", c => c.String(nullable: false));
        }
    }
}
