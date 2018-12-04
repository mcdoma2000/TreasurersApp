using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("CashJournal", Schema = "dbo")]
    public partial class AppCashJournal
    {
        [Key()]
        [Required()]
        public int Id { get; set; }

        public int CheckNumber { get; set; }

        [Required()]
        public int ContributorId { get; set; }

        [Required()]
        public string BahaiId { get; set; }

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
