using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Motion
{
    public class MotionUpdate
    {
        public int MotionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}