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
        public int OriginalOrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DatePassed { get; set; }
        public List<AmendmentDetail> Amendments { get; set; }
    }
}