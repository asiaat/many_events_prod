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
using Serilog;
using ManyEvents.Migrations;

namespace ManyEvents.Controllers
{
    [ApiController]
    [Route("api/mpersons")]
    public class MPersonController : Controller
    {
        private readonly DBContext _context;

        public MPersonController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("persons")]
        public IEnumerable<MPersonDto> GetMPersons()
        {

            Log.Information("MPersonController::GetMPersons ");

            var list = _context.MPerson
                .Select(f => new MPersonDto
                {
                    Id = f.Id,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    /*
                    PersonalCode = new PersonalCode
                    {
                        Code = f.PersonalCode.Code,

                    },
                    */
                   

                }).ToList();

            return list;
        }

        // GET: MPerson/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MPerson == null)
            {
                return NotFound();
            }

            var mPerson = await _context.MPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mPerson == null)
            {
                return NotFound();
            }

            return View(mPerson);
        }

        // GET: MPerson/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: MFeeTypes/Create
        [HttpPost]
        [Route("create")]
        public string CreateNewPerson(ProbeClass person)
        {
            Log.Information("MPersonController::CreateNewPerson " +
                person.FirstName + " " +person.LastName);

            var newPerson = new MPerson();
            newPerson.SetFirstName(person.FirstName);
            newPerson.SetLastName(person.LastName);
            newPerson.SetPersonalCode(person.PersonalCodeAsString);

            var feeType = _context.MFeeType
                .FirstOrDefault();

            newPerson.SetFeeType(feeType);

            _context.Add(newPerson);
            _context.SaveChanges();

            return "OK";
            
        }

        // GET: MPerson/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MPerson == null)
            {
                return NotFound();
            }

            var mPerson = await _context.MPerson.FindAsync(id);
            if (mPerson == null)
            {
                return NotFound();
            }
            return View(mPerson);
        }

        

        // GET: MPerson/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MPerson == null)
            {
                return NotFound();
            }

            var mPerson = await _context.MPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mPerson == null)
            {
                return NotFound();
            }

            return View(mPerson);
        }

        // POST: MPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MPerson == null)
            {
                return Problem("Entity set 'DBContext.MPerson'  is null.");
            }
            var mPerson = await _context.MPerson.FindAsync(id);
            if (mPerson != null)
            {
                _context.MPerson.Remove(mPerson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MPersonExists(int id)
        {
          return (_context.MPerson?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
