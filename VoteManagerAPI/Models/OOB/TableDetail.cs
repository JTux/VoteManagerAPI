using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Motion;

namespace VoteManagerAPI.Models.OOB
{
    public class TableDetail
    {
        public List<MotionDetail> Motions { get; set; }
        public List<AmendmentDetail> Amendments { get; set; }
    }
}