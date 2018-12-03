using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  [Table("User", Schema = "Security")]
  public partial class User
  {
    [Key()]
    [Required()]
    public Guid UserID { get; set; }

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
