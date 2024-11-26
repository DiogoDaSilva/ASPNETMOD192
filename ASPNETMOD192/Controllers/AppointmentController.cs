﻿using ASPNETMOD192.Data;
using ASPNETMOD192.Models;
using ASPNETMOD192.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using static ASPNETMOD192.ASPNETMOD192Constants.POLICIES;

namespace ASPNETMOD192.Controllers
{
    // Roles = "admin,driver,administrative"
    [Authorize(Policy = APP_POLICY.NAME)]
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IToastNotification _toastNotification;
        private readonly IHtmlLocalizer<Resource> _sharedLocalizer;
        private readonly IEmailSender _emailSender;

        private readonly ApplicationDbContext _context;

        public AppointmentController(ILogger<AppointmentController> logger,
                                     IToastNotification toastNotification,
                                     IHtmlLocalizer<Resource> sharedLocalizer,
                                     IEmailSender emailSender,
                                     ApplicationDbContext context)
        {
            _logger = logger;
            _toastNotification = toastNotification;
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _emailSender = emailSender;
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
        [Authorize(Policy = APP_POLICY_EDITABLE_CRUD.NAME)]
        public IActionResult Create()
        {
            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");
            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Policy = APP_POLICY_EDITABLE_CRUD.NAME)]
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

                    string formattedSuccessMessage = string.Format(
                           _sharedLocalizer["Successfully scheduled <br/> <b>Appointment # {0}</b>"].Value,
                           appointment.AppointmentNumber
                    );

                    _toastNotification.AddSuccessToastMessage(formattedSuccessMessage);

                    // Send Email
                    Client? client = _context.Clients.Find(appointment.ClientID);
                    Staff? staff = _context.Staff.Find(appointment.StaffID);

                    if (client == null || staff == null)
                    {
                        return NotFound();
                    }

                    string template = System.IO.File.ReadAllText(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "EmailTemplates",
                            "create_appointment.html"
                        )
                    );


                    StringBuilder htmlBody = new StringBuilder(template);
                    htmlBody.Replace("##CUSTOMER_NAME##", client.Name);
                    htmlBody.Replace("##APPOINTMENT_DATE##", appointment.Date.ToShortDateString());
                    htmlBody.Replace("##APPOINTMENT_TIME##", appointment.Time.ToShortTimeString());
                    htmlBody.Replace("##STAFF_NAME##", staff.Name);

                    var x = _emailSender.SendEmailAsync(client.Email, "Appointment Scheduled", htmlBody.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorRepeatedAppointmentNumber"].Value;
                }
            }

            string formattedMessage = string.Format(_sharedLocalizer["Error while schedulling Appointment #{0}."].Value,
                                                    appointment.AppointmentNumber);


            _toastNotification.AddErrorToastMessage(formattedMessage);

            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");
            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");
            return View(appointment);
        }

        private void SetupAppointments()
        {
            ViewBag.ClientList = new SelectList(_context.Clients, "ID", "Name");

            ViewBag.StaffList = new SelectList(_context.Staff, "ID", "Name");
        }


        [HttpGet]
        [Authorize(Policy = APP_POLICY_EDITABLE_CRUD.NAME)]
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
        [Authorize(Policy = APP_POLICY_EDITABLE_CRUD.NAME)]
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

        [Authorize(Policy = APP_POLICY_ADMIN.NAME)]
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

        [HttpPost, ActionName("Delete")] // POST /Appointment/Delete
        [Authorize(Policy = APP_POLICY_ADMIN.NAME)]
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

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyValidAppointmentNumber(string appointmentNumber)
        {
            bool valid = _context.Appointments
                                    .Where(ap => ap.AppointmentNumber == appointmentNumber)
                                    .IsNullOrEmpty();

            return Json(valid);
        }
    }
}
