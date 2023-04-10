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
using ManyEvents.Migrations;

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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("companies")]
        public async Task<IActionResult> GetCompanies()
        {
 
            var list = await _context.MCompany
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

                }).ToListAsync();

            return Ok(list);
        }

        

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewCompany(MCompanyDto company)
        {
            Log.Information("MPersonController::CreateNewCompany " +
                company.JurName + " " + company.RegCode);

            var newComp = new MCompany();

            newComp.SetJurName(company.JurName);
            newComp.SetReqCode(company.RegCode);
            newComp.SetGuestsCount(company.GuestsCount);
            

            var feeType = _context.MFeeType
                .FirstOrDefault();

            newComp.SetFeeType(feeType);

            _context.Add(newComp);
            await _context.SaveChangesAsync();

            return Ok(newComp);

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCompany(MCompanyDto company)
        {
            var foundCompany = _context.MCompany
                .Where(p => p.Id == company.Id)
                .FirstOrDefault();

            if (foundCompany is null)
            {
                string msg = "Cant found the Company with id: " +
                    company.Id.ToString();

                Log.Error(msg);

                return StatusCode(402, "Company was not found");
            }

            if (foundCompany.JurName is not null)
            {
                foundCompany.SetJurName(company.JurName);
            }

            if (foundCompany.RegCode is not null)
            {
                foundCompany.SetReqCode(company.RegCode);
            }
            if (foundCompany.GuestsCount.ToString() != null)
            {
                foundCompany.SetGuestsCount(company.GuestsCount);
            }

            await _context.SaveChangesAsync();

            return Ok(foundCompany);
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
        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (_context.MCompany == null)
            {
                return Problem("Entity set 'DBContext.MCompany'  is null.");
            }
            var mCompany = await _context.MCompany.FindAsync(id);
            if (mCompany != null)
            {
                _context.MCompany.Remove(mCompany);
                await _context.SaveChangesAsync();

                Log.Information("MCompanyController::Delete (" +
                mCompany.JurName + " , " + mCompany.RegCode + ") was deleted");

                return Ok();
            }
            else
            {
                return NotFound();
            }
            
            
        }

    }
}
