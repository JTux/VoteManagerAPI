namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
                        IsPassed = c.Boolean(),
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.VoteEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsAnonymous = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderOfBusinessEntities", "RuleId", "dbo.RuleEntities");
            DropForeignKey("dbo.RuleEntities", "OriginalOrderId", "dbo.OrderOfBusinessEntities");
            DropForeignKey("dbo.VoteEntities", "VoterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoteEntities", "OrderOfBusinessId", "dbo.OrderOfBusinessEntities");
            DropForeignKey("dbo.OrderOfBusinessEntities", "PresentingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderOfBusinessEntities", "OriginalSessionId", "dbo.SessionEntities");
            DropForeignKey("dbo.SessionEntities", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RuleEntities", new[] { "OriginalOrderId" });
            DropIndex("dbo.VoteEntities", new[] { "VoterId" });
            DropIndex("dbo.VoteEntities", new[] { "OrderOfBusinessId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SessionEntities", new[] { "CreatorId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "RuleId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "PresentingUserId" });
            DropIndex("dbo.OrderOfBusinessEntities", new[] { "OriginalSessionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RuleEntities");
            DropTable("dbo.VoteEntities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SessionEntities");
            DropTable("dbo.OrderOfBusinessEntities");
        }
    }
}
