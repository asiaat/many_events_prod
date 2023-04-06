using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManyEvents.Data;
using ManyEvents.Models;
using ManyEvents.API.Dto;

namespace ManyEvents.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MFeeTypesController : Controller
    {
        private readonly DBContext _context;

        public MFeeTypesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("feetypes")]
        public IEnumerable<MFeeTypeDto> GetMEvents()
        {

            var list = _context.MFeeType
                .Select(f => new MFeeTypeDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Remarks = f.Remarks

                }).ToList();

            return list;
        }

        // POST: MFeeTypes/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("new")]
        public MFeeTypeDto CreateNewFeeType(MFeeType ftype)
        {
            var newFeeType = _context.MFeeType
                .Add(ftype);

            _context.SaveChanges();

            
            return new MFeeTypeDto
            {
                Id = ftype.Id,
                Name = ftype.Name,
                Remarks = ftype.Remarks,
            };
        }

        // GET: MFeeTypes
        public async Task<IActionResult> Index()
        {
              return _context.MFeeType != null ? 
                          View(await _context.MFeeType.ToListAsync()) :
                          Problem("Entity set 'DBContext.MFeeType'  is null.");
        }

        // GET: MFeeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MFeeType == null)
            {
                return NotFound();
            }

            var mFeeType = await _context.MFeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mFeeType == null)
            {
                return NotFound();
            }

            return View(mFeeType);
        }

        // GET: MFeeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MFeeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Remarks")] MFeeType mFeeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mFeeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mFeeType);
        }

        // GET: MFeeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MFeeType == null)
            {
                return NotFound();
            }

            var mFeeType = await _context.MFeeType.FindAsync(id);
            if (mFeeType == null)
            {
                return NotFound();
            }
            return View(mFeeType);
        }

        // POST: MFeeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Remarks")] MFeeType mFeeType)
        {
            if (id != mFeeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mFeeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MFeeTypeExists(mFeeType.Id))
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
            return View(mFeeType);
        }

        // GET: MFeeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MFeeType == null)
            {
                return NotFound();
            }

            var mFeeType = await _context.MFeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mFeeType == null)
            {
                return NotFound();
            }

            return View(mFeeType);
        }

        // POST: MFeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MFeeType == null)
            {
                return Problem("Entity set 'DBContext.MFeeType'  is null.");
            }
            var mFeeType = await _context.MFeeType.FindAsync(id);
            if (mFeeType != null)
            {
                _context.MFeeType.Remove(mFeeType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MFeeTypeExists(int id)
        {
          return (_context.MFeeType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
