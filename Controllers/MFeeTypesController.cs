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
        public IEnumerable<MFeeTypeDto> GetMEvents()
        {
            Log.Information("MFeeTypesController::GetMEvents");


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
        [Route("new")]
        public MFeeTypeDto CreateNewFeeType(MFeeType ftype)
        {
            var newFeeType = _context.MFeeType
                .Add(ftype);

            _context.SaveChanges();

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
        public MFeeTypeDto UpdateFeeType(MFeeType ftype)
        {
            var foundFeeType = _context.MFeeType
                .Where(f => f.Id == ftype.Id)
                .FirstOrDefault();

            if(foundFeeType is null)
            {
                string msg = "Fee Type with this id does not exists id: " +
                    ftype.Id.ToString();

                Log.Error(msg);

                throw new Exception(msg);
            }

            if(ftype.Name is not null)
            {
                foundFeeType.SetName(ftype.Name);
            }

            if(ftype.Remarks is not null)
            {
                foundFeeType.SetRemarks(ftype.Remarks);
            }

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

        
        [HttpDelete]
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

        private bool MFeeTypeExists(int id)
        {
          return (_context.MFeeType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
