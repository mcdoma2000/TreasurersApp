using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Model
{
  public class AppUserEdit
  {
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public ICollection<AppUserClaim> UserClaims { get; set; }

    public AppUserEdit()
    {
      this.UserClaims = new List<AppUserClaim>();
      this.UserName = "unknown";
      this.DisplayName = "Unknown, User";
    }
  }
}
