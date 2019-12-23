using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Entities
{
    public class RuleEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(OrderOfBusiness))]
        public int OriginalOrderId { get; set; }
        public virtual OrderOfBusinessEntity OrderOfBusiness { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset DatePassed { get; set; }

        public virtual ICollection<AmendmentEntity> Ammendments { get; set; }
    }
}