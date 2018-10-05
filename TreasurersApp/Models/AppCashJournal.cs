using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Model
{
    [Table("CashJournal", Schema = "dbo")]
    public partial class AppCashJournal
    {
        [Key()]
        [Required()]
        public int Id { get; set; }

        [Required()]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required()]
        public DateTime CreatedDate { get; set; }

        [Required()]
        [StringLength(255)]
        public string LastModifiedBy { get; set; }

        [Required()]
        public DateTime LastModifiedDate { get; set; }

    }
}
