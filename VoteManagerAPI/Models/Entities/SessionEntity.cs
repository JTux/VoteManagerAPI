using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoteManagerAPI.Models.Entities
{
    public class SessionEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset StartDate { get; set; }

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public virtual ICollection<OrderOfBusinessEntity> OrdersOfBusiness { get; set; }
    }
}