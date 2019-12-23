using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Models.Amendment
{
    public class AmendmentDetail
    {
        public int AmendmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PresenterName { get; set; }
        public bool IsActive { get; set; }
        public bool IsPassed { get; set; }
        public bool IsTabled { get; set; }
        public List<VoteDetail> Votes { get; set; }
        public int RuleId { get; set; }
    }
}