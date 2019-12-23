using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models
{
    public class RoleAssignmentModel
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string RoleName { get; set; }
    }

    public class RoleCreateModel
    {
        [Required]
        public string RoleName { get; set; }
    }

    public class RoleDeleteModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}