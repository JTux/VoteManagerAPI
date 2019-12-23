using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Vote
{
    public class VoteDetail
    {
        public int VoteId { get; set; }
        public int OrderOfBusinessId { get; set; }
        public string VoterName { get; set; }
        public VoteStatus Status { get; set; }
    }
}