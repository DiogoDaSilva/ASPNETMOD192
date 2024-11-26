using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    public class Client
    {
        [Display(Name = "Is Done?")]
        public int ID { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateOnly Birthday { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "VAT #")]
        public string VATNumber { get; set; }

        [Display(Name = "Admission Date")]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Deactivation")]
        public DateTime DeactivationDate { get; set ;}

        [Display(Name = "Cellphone")]
        public string? CellPhoneNumber { get; set; }

        [EmailAddress] // TODO Check me
        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        // CreatedBy TODO FIXME
    }

    

}
