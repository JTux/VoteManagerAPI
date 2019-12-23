namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsPassedToAmendment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderOfBusinessEntities", "IsPassed", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderOfBusinessEntities", "IsPassed");
        }
    }
}
