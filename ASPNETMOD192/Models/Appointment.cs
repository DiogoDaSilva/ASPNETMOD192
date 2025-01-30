using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    [Index(nameof(AppointmentNumber), IsUnique = true)]
    public class Appointment
    {
        public int ID { get; set; }

        [Remote(action: "VerifyValidAppointmentNumber", controller: "Appointment", ErrorMessage = "ErrorRepeatedAppointmentNumber")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        [StringLength(10)]
        [Display(Name = "Appointment #")]
        public string AppointmentNumber { get; set; } 

        [Required(ErrorMessage = "RequiredErrorMessage")] // CTRL + SHIFT + H
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateOnly Date { get; set; }


        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Informations")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Informations { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name ="Is Done?")]
        public bool IsDone { get; set; }


        // Propriedade de Navegação Lazy Loading
        [ValidateNever]
        public Staff Staff { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name ="Staff")]
        public int StaffID { get; set; }

        [ValidateNever]
        public Client Client { get; set; }


        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Client")]
        public int ClientID { get; set; }


        public ICollection<MedicalOperation> MedicalOperations { get; set; }

    }
}
