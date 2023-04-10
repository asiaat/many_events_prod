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

namespace ManyEvents.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MEventsController : Controller
    {
        private readonly DBContext _context;

        public MEventsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("events")]
        public  IEnumerable<MEventDto> GetMEvents()
        {

            var list = _context.MEvent
                .Select(e => new MEventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Place = e.Place,
                    ReleaseDate = e.ReleaseDate,
                    Price = e.Price,
                    EventFeeType = new MFeeTypeDto
                    {
                        Id = e.EventFeeType.Id,
                        Name = e.EventFeeType.Name,
                        Remarks = e.EventFeeType.Remarks
                    },
                    

                    PersonsList = e.MPersons.Select(
                        p => new MPersonDto
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            PersonalCodeAsString = p.PersonalCode.Code,
                        }).ToList(),

                    CompaniesList = e.MCompanies.Select(
                        c => new MCompanyDto
                        {
                            Id = c.Id,
                            JurName = c.JurName,
                            RegCode = c.RegCode,
                            GuestsCount = c.GuestsCount
                        }
                        ).ToList(),

                    //GuestCount = e.MPersons.Count()
                    GuestCount = e.MCompanies.Select(c => c.GuestsCount).Sum()+
                        e.MPersons.Count()



                }).ToList();

            return list;
        }

        // GET: MEvents
        public async Task<IActionResult> Index()
        {
              return _context.MEvent != null ? 
                          View(await _context.MEvent.ToListAsync()) :
                          Problem("Entity set 'DBContext.MEvent'  is null.");
        }

        // GET: MEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MEvent == null)
            {
                return NotFound();
            }

            var mEvent = await _context.MEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mEvent == null)
            {
                return NotFound();
            }

            return View(mEvent);
        }

        // GET: MEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [Route("create")]
        public string CreateNewEvent(MEventDto mevent)
        {
            Log.Information("MPersonController::CreateNewPerson " +
                mevent.Title + " " + mevent.Place);

            var newEvent = new MEvent();
            newEvent.SetTitle(mevent.Title);
            newEvent.SetPlace(mevent.Place);
            newEvent.SetPrice(mevent.Price);
            newEvent.SetReleaseDate(mevent.ReleaseDate);

            var feeType = _context.MFeeType
                .FirstOrDefault();

            newEvent.SetFeeType(feeType);

            _context.Add(newEvent);
            _context.SaveChanges();

            return "OK";

        }

        [HttpGet]
        [Route("bind")]
        public string BindEventWithPerson()
        {
           
            var foundEvent = _context.MEvent
                .Where(e => e.Id == 2)
                .FirstOrDefault();

            var person = _context.MPerson
                .Where(p => p.Id == 1)
                .FirstOrDefault();

            if(foundEvent is not null && person is not null)
            {
                
                person.EventsList
                    .Add(foundEvent);

                _context.SaveChanges();
            }

            

            return "OK";

        }

        [HttpGet]
        [Route("addguest/{eventId:int}/{personId:int}")]
        public string AddGuest(int eventid, int personid)
        {

            var foundEvent = _context.MEvent
                .Where(e => e.Id == eventid)
                .FirstOrDefault();

            var person = _context.MPerson
                .Where(p => p.Id == personid)
                .FirstOrDefault();


            if (foundEvent is not null && person is not null)
            {

                person.EventsList
                    .Add(foundEvent);

                // TODO try catch?
                _context.SaveChanges();
            }



            return "OK";

        }

        // GET: MEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MEvent == null)
            {
                return NotFound();
            }

            var mEvent = await _context.MEvent.FindAsync(id);
            if (mEvent == null)
            {
                return NotFound();
            }
            return View(mEvent);
        }

        // POST: MEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Price")] MEvent mEvent)
        {
            if (id != mEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEventExists(mEvent.Id))
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
            return View(mEvent);
        }

        // GET: MEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MEvent == null)
            {
                return NotFound();
            }

            var mEvent = await _context.MEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mEvent == null)
            {
                return NotFound();
            }

            return View(mEvent);
        }

        // POST: MEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MEvent == null)
            {
                return Problem("Entity set 'DBContext.MEvent'  is null.");
            }
            var mEvent = await _context.MEvent.FindAsync(id);
            if (mEvent != null)
            {
                _context.MEvent.Remove(mEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MEventExists(int id)
        {
          return (_context.MEvent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
