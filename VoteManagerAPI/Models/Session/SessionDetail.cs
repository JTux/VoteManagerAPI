using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Motion;

namespace VoteManagerAPI.Models.Session
{
    public class SessionDetail
    {
        public int SessionId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string CreatorName { get; set; }
        public bool IsActive { get; set; }
        public List<MotionDetail> Motions { get; set; }
        public List<AmendmentDetail> Amendments { get; set; }
    }
}