using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Models.OOB
{
    public abstract class OrderOfBusinessDetail
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PresenterName { get; set; }
        public bool IsActive { get; set; }
        public bool IsTabled { get; set; }
        public int OriginalSessionId { get; set; }
        public List<VoteDetail> Votes { get; set; }
    }
}