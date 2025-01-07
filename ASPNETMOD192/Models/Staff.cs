using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    public class Staff
    {
        [Display(Name = "ID")]
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
        [Display(Name = "VatNumber")]
        public string VATNumber { get; set; }

        [Display(Name = "AdmissionDate")]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "DeactivationDate")]
        public DateTime DeactivationDate { get; set ;}

        [Display(Name = "CellPhoneNumber")]
        public string? CellPhoneNumber { get; set; }

        [EmailAddress] // TODO Check me
        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "StaffNumber")]
        public string StaffNumber { get;set; }


        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Trade")]
        public Trade Trade { get; set; }

    }
}
