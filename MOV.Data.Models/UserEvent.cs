using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MOV.Data.Models
{
    public class UserEvent
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        public DateTime JoinedOn { get; set; }
    }

}