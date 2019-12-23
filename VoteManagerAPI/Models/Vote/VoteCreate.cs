using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Vote
{
    public class VoteCreate
    {
        [Required]
        public int OrderOfBusinessId { get; set; }

        [Required]
        public VoteStatus Status { get; set; }
    }
}