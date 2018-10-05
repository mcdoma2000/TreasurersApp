using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Model
{
  [Table("User", Schema = "Security")]
  public partial class AppUser
  {
    [Key()]
    [Required()]
    public Guid UserId { get; set; }

    [Required()]
    [StringLength(255)]
    public string UserName { get; set; }

    [Required()]
    [StringLength(255)]
    public string DisplayName { get; set; }

    [Required()]
    [StringLength(255)]
    public string Password { get; set; }
  }
}
