using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Amendment
{
    public class AmendmentUpdate
    {
        public int AmendmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}