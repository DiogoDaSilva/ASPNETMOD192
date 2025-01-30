using ASPNETMOD192.Data;
using ASPNETMOD192.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace ASPNETMOD192.Controllers
{
    public class MedicalOperationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalOperationController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectList appointmentList = new SelectList(_context.Appointments, "ID", "AppointmentNumber");
            
            ViewBag.AppointmentList = appointmentList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(MedicalOperation medicalOperation)
        {
            ICollection<Appointment> dbSelectedAppointments = _context.Appointments
                                                            .Where(ap => medicalOperation.SelectedAppointmentIDs.Contains(ap.ID))
                                                            .ToList();

            medicalOperation.Appointments = dbSelectedAppointments;

            _context.MedicalOperation.Add(medicalOperation);

            _context.SaveChanges();

            return View();
        }
    }
}
