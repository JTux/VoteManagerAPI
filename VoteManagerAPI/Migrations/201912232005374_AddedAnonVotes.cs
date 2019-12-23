namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnonVotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoteEntities", "IsAnonymous", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VoteEntities", "IsAnonymous");
        }
    }
}
