using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Amendment;

namespace VoteManagerAPI.Models.Rule
{
    public class RuleDetail
    {
        public int RuleId { get; set; }
        public int MotionId { get; set; }
        public string MotionTitle { get; set; }
        public DateTimeOffset DatePassed { get; set; }
        public List<AmendmentDetail> Amendments { get; set; }
    }
}