using ASPNETMOD192.Data;
using ASPNETMOD192.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Data.Common;

namespace ASPNETMOD192.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

        private readonly ApplicationDbContext _context;


        public ClientController(ILogger<ClientController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        // CRUD
        // Create
        // Read
        // Update
        // Delete


        // Read All
        public IActionResult Index()
        {
            IEnumerable<Client> clients = _context.Clients.ToList();


            return View(clients);
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client newClient)
        {
            _context.Clients.Add(newClient); // Registo da operação "Guardar Novo Cliente"
            _context.SaveChanges(); // Execução das operações registadas



            return RedirectToAction(nameof(Index));
        }

        
        // Update
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Client? client = _context.Clients.Find(id);


            if (client is null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client updatingClient)
        {
            //Client? clientDB = _context.Clients.Find(updatingClient.ID);


            //if (clientDB is null)
            //{
            //    return NotFound();
            //}

            try
            {
                _context.Clients.Update(updatingClient);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(); // FIXME
            }

            return RedirectToAction(nameof(Index));
        }

        // Read
        public IActionResult Details(int id)
        {
            Client? client = _context.Clients.Find(id);


            if (client is null)
            {
                return NotFound();
            }

            return View(client);
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Client? client = _context.Clients.Find(id);

            if (client is null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Client? client = _context.Clients.Find(id);

            if (client is null)
            {
                return NotFound();
            }

            try
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(client);

        }

        public IActionResult NextAppointments(int id)
        {

            Client? client = _context.Clients
                                            .Include(c => c.Appointments
                                                                .OrderBy(ap => ap.Date)
                                                                .ThenBy(ap => ap.Time)
                                                                .Where(ap => ap.Date >= DateOnly.FromDateTime(DateTime.Now))
                                            )
                                                .ThenInclude(ap => ap.Staff)
                                            .Where(c => c.ID == id)
                                            
                                            .FirstOrDefault();
            
            if (client is null)
            {
                return NotFound();
            }

            return View(client);
        }

        public IActionResult HistoricAppointments(int id)
        {

            Client? client = _context.Clients
                                        .Include(c => c.Appointments
                                                        .OrderBy(ap => ap.Date)
                                                        .ThenBy(ap => ap.Time)
                                                        .Where(ap => ap.Date < DateOnly.FromDateTime(DateTime.Now))
                                                        .Where(ap => ap.IsDone)
                                        )
                                            .ThenInclude(ap => ap.Staff)
                                        .Where(c => c.ID == id)

                                        .FirstOrDefault();

            if (client is null)
            {
                return NotFound();
            }

            return View(client);
        }
    }
}
