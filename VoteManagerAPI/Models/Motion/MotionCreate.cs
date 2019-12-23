using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Motion
{
    public class MotionCreate
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}