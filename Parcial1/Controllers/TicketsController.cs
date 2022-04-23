using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1.Data;
using Parcial1.Data.Entities;
using Parcial1.Helpers;
using Parcial1.Models;

namespace Parcial1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public TicketsController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public IActionResult Validation()
        {
            return View(new TicketViewModel());
        }

        [HttpPost]
        public IActionResult Validation(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int modelId = Convert.ToInt32(model.Id);
                    if ( _context.Tickets.FirstOrDefault(t => t.Id == modelId) != null)
                    {
                        Ticket ticket =  _context.Tickets.FirstOrDefault(t => t.Id == modelId);

                        if (ticket.WasUsed == false)
                        {
                            return RedirectToAction("ModifyTicket", "Tickets", new { modelId = modelId });
                        }
                        else
                        {
                            return RedirectToAction("ShowTicket", "Tickets", new { modelId = modelId });
                        }
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            ModelState.AddModelError(string.Empty, "Este ID no existe. Por favor ingrese un ID válido.");

            return View(model);
        }

        public async Task<IActionResult> ShowTicket(int modelId)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == modelId);
            return View(ticket);
        }

        public async Task<IActionResult> ModifyTicket(int modelId)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == modelId);
            FormatTicketViewModel model = new FormatTicketViewModel()
            {
                Id = ticket.Id,
                WasUsed = true,
                Entrances = await _combosHelper.GetComboEntrancesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyTicket(FormatTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new()
                    {
                        Id = model.Id,
                        WasUsed = model.WasUsed,
                        Document = model.Document,
                        Name = model.Name,
                        Entrace = await _context.Entrances.FindAsync(model.EntranceId),
                        DateTime = DateTime.Now,
                    };

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Entrances = await _combosHelper.GetComboEntrancesAsync();
            return View(model);
        }
    }
}
