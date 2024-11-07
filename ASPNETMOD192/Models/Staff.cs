using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    public class Staff
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthday { get; set; }

        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Required]
        public string VATNumber { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime DeactivationDate { get; set ;}

        public string? CellPhoneNumber { get; set; }

        [EmailAddress] // TODO Check me
        [Required]
        public string Email { get; set; }

        [Required]
        public string StaffNumber { get;set; }
    }
}
