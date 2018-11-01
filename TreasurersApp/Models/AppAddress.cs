using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("Address", Schema = "dbo")]
    public partial class AppAddress
    {
        [Key]
        public int Id { get; set; }

        [Required()]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string AddressLine3 { get; set; }

        [Required()]
        [StringLength(100)]
        public string City { get; set; }

        [Required()]
        [StringLength(100)]
        public string State { get; set; }

        [Required()]
        [StringLength(100)]
        public string PostalCode { get; set; }

    }
}
