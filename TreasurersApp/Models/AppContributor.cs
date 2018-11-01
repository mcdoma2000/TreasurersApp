using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasurersApp.Models
{
    [Table("Contributor", Schema = "dbo")]
    public partial class AppContributor
    {
        [Key]
        public int Id { get; set; }

        [Required()]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required()]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required()]
        public int? AddressId { get; set; }

    }
}
