using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Models.Motion
{
    public class MotionDetail : OrderOfBusinessDetail
    {
        public int MotionId { get; set; }
    }
}