using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  public class UserEdit
  {
    public Guid UserID { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public ICollection<Claim> Claims { get; set; }

    public UserEdit()
    {
      this.Claims = new List<Claim>();
      this.UserName = "unknown";
      this.DisplayName = "Unknown, User";
    }
  }
}
