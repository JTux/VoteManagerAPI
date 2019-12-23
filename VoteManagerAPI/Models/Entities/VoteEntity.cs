using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Entities
{
    public class VoteEntity
    {
        [Key]
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool IsFor { get; set; }

        [ForeignKey(nameof(OrderOfBusiness))]
        public int OrderOfBusinessId { get; set; }
        public virtual OrderOfBusinessEntity OrderOfBusiness { get; set; }

        [ForeignKey(nameof(Voter))]
        public string VoterId { get; set; }
        public virtual ApplicationUser Voter { get; set; }
    }
}