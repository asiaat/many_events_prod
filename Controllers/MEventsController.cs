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
        public  async Task<IActionResult> GetMEvents()
        {

            var list = await _context.MEvent
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
                  
                    GuestCount = e.MCompanies.Select(c => c.GuestsCount).Sum()+
                        e.MPersons.Count()

                }).ToListAsync();

            return Ok(list);
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewEvent(MEventDto mevent)
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
            await _context.SaveChangesAsync();

            return Ok(newEvent);

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
        public async Task<IActionResult> AddGuest(int eventid, int personid)
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
                await _context.SaveChangesAsync();

                return Ok(person);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }           

        }

        [HttpGet]
        [Route("addcompany/{eventId:int}/{companyId:int}")]
        public async Task<IActionResult> AddCompany(int eventid, int companyId)
        {

            var foundEvent = _context.MEvent
                .Where(e => e.Id == eventid)
                .FirstOrDefault();

            var company = _context.MCompany
                .Where(p => p.Id == companyId)
                .FirstOrDefault();

            if (foundEvent is not null && company is not null)
            {
                company.EventsList
                    .Add(foundEvent);

                await _context.SaveChangesAsync();

                return Ok(company);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
