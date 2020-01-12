using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Session
{
    public class SessionListItem
    {
        public int SessionId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string CreatorName { get; set; }
        public bool IsActive { get; set; }
        public int MotionCount { get; set; }
        public int AmendmentCount { get; set; }
    }
}