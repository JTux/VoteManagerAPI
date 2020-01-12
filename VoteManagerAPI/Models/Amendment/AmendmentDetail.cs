using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Models.Amendment
{
    public class AmendmentDetail : OrderOfBusinessDetail
    {
        public int AmendmentId { get; set; }
        public int RuleId { get; set; }
        public bool IsPassed { get; set; }
    }
}