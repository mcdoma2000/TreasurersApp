using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  public class SecurityUserEdit
  {
    public Guid UserID { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public ICollection<SecurityClaim> Claims { get; set; }

    public SecurityUserEdit()
    {
      this.Claims = new List<SecurityClaim>();
      this.UserName = "unknown";
      this.DisplayName = "Unknown, User";
    }
  }
}
