using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Men_Of_Varna.Data.Models
{
    public class UserDestination
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public Destination Destination { get; set; } = null!;
    }
}
