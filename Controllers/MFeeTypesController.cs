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
    public class MFeeTypesController : Controller
    {
        private readonly DBContext _context;

        public MFeeTypesController(DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("feetypes")]
        public async Task<IActionResult> GetFeeTypes()
        {
            var list =  await _context.MFeeType
                .Select(f => new MFeeTypeDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Remarks = f.Remarks

                }).ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        [Route("new")]
        public async Task<MFeeTypeDto> CreateNewFeeType(MFeeType ftype)
        {
            var newFeeType =  _context.MFeeType
                .Add(ftype);

            await _context.SaveChangesAsync();

            Log.Information("MFeeTypesController::CreateNewFeeType "+
                ftype.Name + " , "+ ftype.Remarks);
            
            return new MFeeTypeDto
            {
                Id = ftype.Id,
                Name = ftype.Name,
                Remarks = ftype.Remarks,
            };
        }

      
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MFeeTypeDto> UpdateFeeType(MFeeType ftype)
        {
            var foundFeeType = _context.MFeeType
                .Where(f => f.Id == ftype.Id)
                .FirstOrDefault();

            if(foundFeeType is null)
            {
                string msg = "Fee Type with this id does not exists id: " +
                    ftype.Id.ToString();

                Log.Error(msg);

                //throw new HttpProtocolException(StatusCodes.Status404NotFound,"Object not found",null);
                //return StatusCode(400, "FeeType Not found");
            }

            if(ftype.Name is not null)
            {
                foundFeeType.SetName(ftype.Name);
            }

            if(ftype.Remarks is not null)
            {
                foundFeeType.SetRemarks(ftype.Remarks);
            }

            await _context.SaveChangesAsync();

            return new MFeeTypeDto
            {
                Id = ftype.Id,
                Name = ftype.Name,
                Remarks = ftype.Remarks,
            };
        }


        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.MFeeType == null)
            {
                return Problem("Entity set 'DBContext.MFeeType'  is null.");
            }
            var mFeeType = await _context.MFeeType.FindAsync(id);
            if (mFeeType != null)
            {
                _context.MFeeType.Remove(mFeeType);
                await _context.SaveChangesAsync();

                Log.Information("MFeeTypesController::Delete (" +
                 mFeeType.Name + " , " + mFeeType.Remarks + ") was deleted");

                return Ok();
            }
            else
            {
                return NotFound();
            }
            
            
            
        }

    }
}
