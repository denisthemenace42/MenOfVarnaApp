using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Data.Models
{
    public class UserEvent
    {
        public string UserId { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;

        
        public int EventId { get; set; }

       
        public Event Event { get; set; } = null!;

        public DateTime JoinedOn { get; set; } 
    }

}