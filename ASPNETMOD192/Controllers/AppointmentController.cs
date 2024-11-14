using ASPNETMOD192.Data;
using ASPNETMOD192.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Generic;
using System.Data.Common;

namespace ASPNETMOD192.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IToastNotification _toastNotification;

        private readonly ApplicationDbContext _context;

        public AppointmentController(ILogger<AppointmentController> logger,
                                     IToastNotification toastNotification,
                                     ApplicationDbContext context)
        {
            _logger = logger;
            _toastNotification = toastNotification;
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

                    _toastNotification.AddSuccessToastMessage($"Successfully scheduled <br/> <b>Appointment # {appointment.AppointmentNumber}</b>");

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "This Appointment Number is already used! Please insert a new one.";
                }
            }

            _toastNotification.AddErrorToastMessage($"Error while schedulling Appointment #{appointment.AppointmentNumber}.");
            
            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");
            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");
            return View(appointment);
        }

        private void SetupAppointments()
        {
            ViewBag.CustomerList = new SelectList(_context.Clients, "ID", "Name");

            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Appointment? appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            this.SetupAppointments();
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();


                string message =
                    string.Format("<b>Appointment # {0}</b> successfully edited!",
                                  appointment.AppointmentNumber);


                message += "<br />" + string.Format("Date: <b>{0}</b> at <b>{1}</b>",
                                  appointment.Date.ToShortDateString(),
                                  appointment.Time.ToShortTimeString());

                _toastNotification.AddSuccessToastMessage(message,
                    new ToastrOptions
                    {
                        Title = "Success",
                        TimeOut = 0,
                        TapToDismiss = true
                    });

                return RedirectToAction(nameof(Index));
            }

            this.SetupAppointments();
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Appointment? appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            this.SetupAppointments();
            return View(appointment);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment? customer = _context.Appointments.Find(id);

            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Appointment? appointment = _context.Appointments.Find(id);

            if (appointment is null)
            {
                return NotFound();
            }

            try
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(appointment);

        }
    }
}
