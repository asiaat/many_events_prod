using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManyEvents.Data;
using ManyEvents.Models;
using Serilog;
using ManyEvents.API.Dto;

namespace ManyEvents.Controllers
{
    [ApiController]
    [Route("api/mcompanies")]
    public class MCompanyController : Controller
    {
        private readonly DBContext _context;

        public MCompanyController(DBContext context)
        {
            _context = context;
        }

        // GET: MCompany
        public async Task<IActionResult> Index()
        {
              return _context.MCompany != null ? 
                          View(await _context.MCompany.ToListAsync()) :
                          Problem("Entity set 'DBContext.MCompany'  is null.");
        }

        [HttpGet]
        [Route("companies")]
        public IEnumerable<MCompanyDto> GetCompanies()
        {

            Log.Information("MPersonController::GetMPersons ");

            //var evenList = 
            var list = _context.MCompany
                .Select(f => new MCompanyDto
                {
                    Id = f.Id,
                    JurName = f.JurName,
                    RegCode = f.RegCode,
                    GuestsCount = f.GuestsCount,
                    Remarks = f.Remarks,
                   
                    FeeType = new MFeeTypeDto
                    {
                        Id = f.FeeType.Id,
                        Name = f.FeeType.Name,
                        Remarks = f.FeeType.Remarks,
                    },

                    EventsList = f.EventsList.Select(
                        e => new MEventDto
                        {
                            Id = e.Id,
                            Title = e.Title,
                            Place = e.Place,
                            Price = e.Price

                        }).ToList()

                }).ToList();

            return list;
        }

        // GET: MCompany/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MCompany == null)
            {
                return NotFound();
            }

            var mCompany = await _context.MCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mCompany == null)
            {
                return NotFound();
            }

            return View(mCompany);
        }

        // GET: MCompany/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MCompany/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JurName,RegCode,GuestsCount,Remarks,Id")] MCompany mCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mCompany);
        }

        // GET: MCompany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MCompany == null)
            {
                return NotFound();
            }

            var mCompany = await _context.MCompany.FindAsync(id);
            if (mCompany == null)
            {
                return NotFound();
            }
            return View(mCompany);
        }

        // POST: MCompany/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JurName,RegCode,GuestsCount,Remarks,Id")] MCompany mCompany)
        {
            if (id != mCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MCompanyExists(mCompany.Id))
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
            return View(mCompany);
        }

        // GET: MCompany/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MCompany == null)
            {
                return NotFound();
            }

            var mCompany = await _context.MCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mCompany == null)
            {
                return NotFound();
            }

            return View(mCompany);
        }

        // POST: MCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MCompany == null)
            {
                return Problem("Entity set 'DBContext.MCompany'  is null.");
            }
            var mCompany = await _context.MCompany.FindAsync(id);
            if (mCompany != null)
            {
                _context.MCompany.Remove(mCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MCompanyExists(int id)
        {
          return (_context.MCompany?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
