using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using VoteManagerAPI.Models;

[assembly: OwinStartup(typeof(VoteManagerAPI.Startup))]

namespace VoteManagerAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ValidateDefaultRoles();
        }

        private void ValidateDefaultRoles()
        {
            var context = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole { Name = "Admin" });

            if (!roleManager.RoleExists("Chair"))
                roleManager.Create(new IdentityRole { Name = "Chair" });

            if (!roleManager.RoleExists("Founder"))
                roleManager.Create(new IdentityRole { Name = "Founder" });

            if (!roleManager.RoleExists("Member"))
                roleManager.Create(new IdentityRole { Name = "Member" });

            if (!roleManager.RoleExists("Observer"))
                roleManager.Create(new IdentityRole { Name = "Observer" });

            if (userManager.FindByEmail("admin@vmapi.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "SrAdmin",
                    Email = "admin@vmapi.com",
                    EmailConfirmed = true,
                    FirstName = "Master",
                    LastName = "Admin"
                };

                var adminCreateResult = userManager.Create(adminUser, "password");

                if (adminCreateResult.Succeeded)
                {
                    var roleAssignResult = userManager.AddToRole(adminUser.Id, "Admin");
                    if (!roleAssignResult.Succeeded)
                    {
                        throw new Exception("Cannot assign admin Role to existing Admin account.");
                    }
                }
                else
                {
                    throw new Exception("Cannot create base admin account.");
                }
            }
        }
    }
}
