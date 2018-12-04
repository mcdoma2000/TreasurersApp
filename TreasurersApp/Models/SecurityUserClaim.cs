using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("UserClaim", Schema = "Security")]
    public class SecurityUserClaim
    {
        [Required()]
        [Key()]
        public Guid UserClaimID { get; set; }

        [Required()]
        public Guid ClaimID { get; set; }

        [Required()]
        public Guid UserID { get; set; }
    }
}
