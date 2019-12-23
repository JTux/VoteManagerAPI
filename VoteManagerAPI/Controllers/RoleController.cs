using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VoteManagerAPI.Models;

namespace VoteManagerAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public RoleController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ApplicationRoleManager RoleManager
        {
            get => _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>();
            private set => _roleManager = value;
        }

        // Add User to Role
        [HttpPost]
        public IHttpActionResult AddUserToRole(RoleAssignmentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var user = UserManager.FindByEmail(model.UserEmail);
            if (user == null)
                return BadRequest("No user with given email exists.");

            if (UserManager.GetRoles(user.Id).Contains(model.RoleName))
                return BadRequest($"User already in {model.RoleName} role.");

            var assignmentResult = UserManager.AddToRole(user.Id, model.RoleName);
            if (!assignmentResult.Succeeded)
                return InternalServerError(new Exception($"Cannot assign {model.RoleName} to user {model.UserEmail}."));

            return Ok();
        }

        // Remove User from Role
        [HttpDelete]
        public IHttpActionResult RemoveUserFromRole(RoleAssignmentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var user = UserManager.FindByEmail(model.UserEmail);
            if (user == null)
                return BadRequest("No user with given email exists.");

            if (!UserManager.GetRoles(user.Id).Contains(model.RoleName))
                return BadRequest($"User is not in {model.RoleName} role.");

            if (User.Identity.GetUserId() == user.Id && model.RoleName.ToLower() == "admin")
                return BadRequest("Cannot remove self from Admin role.");

            var assignmentResult = UserManager.RemoveFromRole(user.Id, model.RoleName);
            if (!assignmentResult.Succeeded)
                return InternalServerError(new Exception($"Cannot remove user {model.UserEmail} from {model.RoleName} role."));

            return Ok();
        }

        // Create Role
        [HttpPost]
        public IHttpActionResult CreateRole(RoleCreateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var existingRole = RoleManager.FindByName(model.RoleName);
            if (existingRole != null)
                return BadRequest("Role already exists.");

            var roleCreateResult = RoleManager.Create(new IdentityRole { Name = model.RoleName });
            if (!roleCreateResult.Succeeded)
                return InternalServerError(new Exception($"Cannot create role with name {model.RoleName}."));

            return Ok();
        }

        // Delete Created Role
        [HttpDelete]
        public IHttpActionResult DeleteCustomRole(RoleDeleteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var existingRole = RoleManager.FindByName(model.RoleName);
            if (existingRole == null)
                return BadRequest("Role does not exist.");

            if (new string[] { "admin", "chair", "founder", "member", "observer" }.Contains(model.RoleName.ToLower()))
                return BadRequest("Cannot delete default roles.");

            var roleDeleteResult = RoleManager.Delete(existingRole);
            if (!roleDeleteResult.Succeeded)
                return InternalServerError(new Exception($"Cannot delete role with name {model.RoleName}."));

            return Ok();
        }
    }
}
