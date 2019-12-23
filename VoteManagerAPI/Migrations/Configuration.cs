namespace VoteManagerAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VoteManagerAPI.Models.Entities;
    using VoteManagerAPI.Models.Vote;

    internal sealed class Configuration : DbMigrationsConfiguration<VoteManagerAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VoteManagerAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //var user = context.Users.FirstOrDefault(u => u.Email == "admin@vmapi.com");
            //if (user != null)
            //{
            //    var exampleSession = new SessionEntity { CreatorId = user.Id, StartDate = DateTime.UtcNow, IsActive = true };
            //    var exampleMotion = new MotionEntity { Title = "Example Motion", Description = "This is an example motion.", PresentingUserId = user.Id, OriginalSessionId = exampleSession.Id, IsActive = true };

            //    context.Sessions.AddOrUpdate(exampleSession);
            //    context.OrdersOfBusiness.AddOrUpdate(exampleMotion);
            //    context.Votes.AddOrUpdate(new VoteEntity { Status = VoteStatus.Aye, OrderOfBusinessId = exampleMotion.Id, VoterId = user.Id }, new VoteEntity { Status = VoteStatus.Nay, OrderOfBusinessId = exampleMotion.Id, VoterId = user.Id });
            //    context.Rules.AddOrUpdate(new RuleEntity { DatePassed = DateTime.UtcNow, OriginalOrderId = exampleMotion.Id });
            //}
        }
    }
}
