using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
  [Table("Report", Schema = "dbo")]
  public partial class Report
  {
    public int Id { get; set; }

    [Required()]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(256)]
    public string DisplayName { get; set; }

    [StringLength(2048)]
    public string ConfigurationJson { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public int DisplayOrder { get; set; }
  }
}
