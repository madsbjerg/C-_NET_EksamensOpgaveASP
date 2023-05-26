using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BilReperationFirmaWebApp.DAL;
using BilReperationFirmaWebApp.Models;

namespace BilReperationFirmaWebApp.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly BilFirmaContext _context;

        public MechanicsController(BilFirmaContext context)
        {
            _context = context;
        }

        // GET: Mechanics
        public async Task<IActionResult> Index()
        {
              return _context.Mechanics != null ? 
                          View(await _context.Mechanics.ToListAsync()) :
                          Problem("Entity set 'BilFirmaContext.Mechanics'  is null.");
        }

        // GET: Mechanics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .Include(m => m.Orders)
                .ThenInclude(o => o.OrderTypes)
                .ThenInclude(ot => ot.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // GET: Mechanics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mechanics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HiringDate,Salary")] Mechanic mechanic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mechanic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mechanic);
        }

        // GET: Mechanics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null)
            {
                return NotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,HiringDate,Salary")] Mechanic mechanic)
        {
            if (id != mechanic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanicExists(mechanic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mechanic);
        }

        // GET: Mechanics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // POST: Mechanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mechanics == null)
            {
                return Problem("Entity set 'BilFirmaContext.Mechanics'  is null.");
            }
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic != null)
            {
                _context.Mechanics.Remove(mechanic);
            }

            // Remove Order links to removed customer
            var orders = await _context.Orders.Where(o => o.MechanicId == id).ToListAsync();
            foreach (var order in orders)
            {
                order.MechanicId = null;
                order.Mechanic = null;
                _context.Update(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanicExists(int id)
        {
          return (_context.Mechanics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
