namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedVoteStatusType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoteEntities", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.VoteEntities", "IsFor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VoteEntities", "IsFor", c => c.Boolean(nullable: false));
            DropColumn("dbo.VoteEntities", "Status");
        }
    }
}
