using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Entities
{
    public abstract class OrderOfBusinessEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsTabled { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [ForeignKey(nameof(OriginalSession))]
        public int OriginalSessionId { get; set; }
        public virtual SessionEntity OriginalSession { get; set; }

        [ForeignKey(nameof(PresentingUser))]
        public string PresentingUserId { get; set; }
        public virtual ApplicationUser PresentingUser { get; set; }

        public virtual ICollection<VoteEntity> Votes { get; set; }
    }

    public class MotionEntity : OrderOfBusinessEntity
    {
        public override ICollection<VoteEntity> Votes { get; set; }
    }

    public class AmendmentEntity : OrderOfBusinessEntity
    {
        [ForeignKey(nameof(Rule))]
        public int RuleId { get; set; }
        public virtual RuleEntity Rule { get; set; }

        public override ICollection<VoteEntity> Votes { get; set; }
    }
}