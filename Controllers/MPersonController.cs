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
using System.Net;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;

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
        public async Task<IActionResult> GetMPersons()
        {

            //Log.Information("MPersonController::GetMPersons ");

            var list = await _context.MPerson
                .Select(f => new MPersonDto
                {
                    Id = f.Id,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    PersonalCodeAsString = f.PersonalCode.Code,
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

                }).ToListAsync();

            return Ok(list);
        }


        // POST: MFeeTypes/Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewPerson(MPersonDto person)
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

            try
            {
                _context.Add(newPerson);
                await _context.SaveChangesAsync();

                return Ok(newPerson);
            }
            catch
            {
                return StatusCode(500, "Internal Server error");
            }
            

            
            
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult>  UpdatePerson(MPersonDto person)
        {
            var foundPerson = _context.MPerson
                .Where(p => p.Id == person.Id)
                .FirstOrDefault();

            if (foundPerson is null)
            {
                string msg = "Cant found the person with id: " +
                    person.Id.ToString();

                Log.Error(msg);

                return StatusCode(402, "Person not found");
            }

            if (foundPerson.FirstName is not null)
            {
                foundPerson.SetFirstName(person.FirstName);
            }

            if (foundPerson.LastName is not null)
            {
                foundPerson.SetLastName(person.LastName);
            }

            await _context.SaveChangesAsync();


            return Ok(foundPerson);
        }

        

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.MPerson == null)
            {
                return Problem("Entity set 'DBContext.MPerson'  is null.");
            }
            var mPerson = await _context.MPerson.FindAsync(id);
            if (mPerson != null)
            {
                _context.MPerson.Remove(mPerson);
                await _context.SaveChangesAsync();

                Log.Information("MPersonController::Delete (" +
                 mPerson.FirstName + " , " + mPerson.LastName + ") was deleted");

                return Ok();
            }
            else
            {
                return NotFound();
            }


        }

        
    }
}
