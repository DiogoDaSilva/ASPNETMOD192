using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    public class Client
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateOnly Birthday { get; set; }

        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "VAT #")]
        public string VATNumber { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime DeactivationDate { get; set ;}

        [Display(Name = "Cellphone")]
        public string? CellPhoneNumber { get; set; }

        [EmailAddress] // TODO Check me
        [Required]
        public string Email { get; set; }

        // CreatedBy TODO FIXME
    }

    

}
