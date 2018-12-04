using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  [Table("Claim", Schema = "Security")]
  public class AppClaim
  {
    [Required()]
    [Key()]
    public int Id { get; set; }

    [Required()]
    [StringLength(50)]
    public string ClaimName { get; set; }

    [Required()]
    [StringLength(50)]
    public string ClaimValue { get; set; }
  }

}
