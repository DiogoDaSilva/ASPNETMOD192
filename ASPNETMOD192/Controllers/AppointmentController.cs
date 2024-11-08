using ASPNETMOD192.Data;
using ASPNETMOD192.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ASPNETMOD192.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;

        private readonly ApplicationDbContext _context;

        public AppointmentController(ILogger<AppointmentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            IEnumerable<Appointment> appointments = _context.Appointments
                                                            .Include(ap => ap.Client)
                                                            .Include(ap => ap.Staff)
                                                            .ToList();
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");
            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Appointment> repeatedAppointmentNumbers = _context.Appointments
                                            .Where(ap => ap.AppointmentNumber == appointment.AppointmentNumber)
                                            .ToList();

                if (repeatedAppointmentNumbers.Count() == 0)
                {
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "This Appointment Number is already used! Please insert a new one.";
                }
            }

            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");
            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");
            return View(appointment);
        }
    }
}
