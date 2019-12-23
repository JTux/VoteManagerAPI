using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Models.Entities
{
    public class VoteEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsAnonymous { get; set; }

        [Required]
        public VoteStatus Status { get; set; }

        [ForeignKey(nameof(OrderOfBusiness))]
        public int OrderOfBusinessId { get; set; }
        public virtual OrderOfBusinessEntity OrderOfBusiness { get; set; }

        [ForeignKey(nameof(Voter))]
        public string VoterId { get; set; }
        public virtual ApplicationUser Voter { get; set; }
    }
}