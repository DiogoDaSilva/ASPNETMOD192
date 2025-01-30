using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETMOD192.Models
{

    public class MedicalOperation
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "RequiredErrorMessage")]
        [Display(Name = "Informations")]
        public string Informations { get; set; }


        public ICollection<Appointment> Appointments { get; set; }

        [ValidateNever]
        [NotMapped]
        public int[] SelectedAppointmentIDs { get; set; }
    }
}
