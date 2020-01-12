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
            //This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
