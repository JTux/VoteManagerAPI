namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEntitiesToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderOfBusinessEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IsTabled = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OriginalSessionId = c.Int(nullable: false),
                        PresentingUserId = c.String(maxLength: 128),
                        RuleId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SessionEntities", t => t.OriginalSessionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PresentingUserId)
                .ForeignKey("dbo.RuleEntities", t => t.RuleId, cascadeDelete: true)
                .Index(t => t.OriginalSessionId)
                .Index(t => t.PresentingUserId)
                .Index(t => t.RuleId);
            
            CreateTable(
                "dbo.SessionEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatorId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.VoteEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsFor = c.Boolean(nullable: false),
                        OrderOfBusinessId = c.Int(nullable: false),
                        VoterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderOfBusinessEntities", t => t.OrderOfBusinessId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.VoterId)
                .Index(t => t.OrderOfBusinessId)
                .Index(t => t.VoterId);
            
            CreateTable(
                "dbo.RuleEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalOrderId = c.Int(nullable: false),
                        DatePassed = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderOfBusinessEntities", t => t.OriginalOrderId, cascadeDelete: false)
                .Index(t => t.OriginalOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderOfBusinessEntities", "RuleId", "dbo.RuleEntities");
            DropForeignKey("dbo.RuleEntities", "OriginalOrderId", "dbo.OrderOfBusinessEntities");
            DropForeignKey("dbo.VoteEntities", "VoterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoteEntities", "OrderOfBusinessId", "dbo.OrderOfBusinessEntities");
            DropForeignKey("dbo.OrderOfBusinessEntities", "PresentingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderOfBusinessEntities", "OriginalSessionId", "dbo.SessionEntities");
            DropForeignKey("dbo.SessionEntities", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.RuleEntities", new[] { "OriginalOrderId" });
            DropIndex("dbo.VoteEntities", new[] { "VoterId" });
            DropIndex("dbo.VoteEntities", new[] { "OrderOfBusinessId" });
            DropIndex("dbo.SessionEntities", new[] { "CreatorId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "RuleId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "PresentingUserId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "OriginalSessionId" });
            DropTable("dbo.RuleEntities");
            DropTable("dbo.VoteEntities");
            DropTable("dbo.SessionEntities");
            DropTable("dbo.OrderOfBusinessEntities");
        }
    }
}
