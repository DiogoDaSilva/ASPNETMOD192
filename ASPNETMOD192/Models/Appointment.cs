﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMOD192.Models
{
    [Index(nameof(AppointmentNumber), IsUnique = true)]
    public class Appointment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Appointment #")]
        public string AppointmentNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateOnly Date { get; set; }


        [Required]
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Informations")]
        public string Informations { get; set; }

        [Required]
        [Display(Name ="Is Done?")]
        public bool IsDone { get; set; }


        // Propriedade de Navegação Lazy Loading
        [ValidateNever]
        public Staff Staff { get; set; }

        [Required]
        [Display(Name ="Staff")]
        public int StaffID { get; set; }

        [ValidateNever]
        public Client Client { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientID { get; set; }

    }
}
