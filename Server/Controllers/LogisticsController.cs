using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTech.Server.Data;
using ProjectTech.Server.IRepository;
using ProjectTech.Shared.Domain;

namespace ProjectTech.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public LogisticsController(ApplicationDbContext context)
        public LogisticsController(IUnitOfWork unitOfWork)
        {
            //    _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Logistics
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Logistic>>> GetLogistics()
        public async Task<IActionResult> GetLogistics()
        {
            //return await _context.Logistics.ToListAsync();
            var logistics = await _unitOfWork.Logistics.GetAll();
            return Ok(logistics);
        }

        // GET: api/Logistics/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Logistic>> GetLogistic(int id)
        public async Task<IActionResult> GetLogistic(int logisticsId)    
        {
            //var logistic = await _context.Logistics.FindAsync(id);
            var logistic = await _unitOfWork.Logistics.Get(q => q.LogisticId == logisticsId);

            if (logistic == null)
            {
                return NotFound();
            }

            //return logistic;
            return Ok(logistic);
        }

        // PUT: api/Logistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogistic(int id, Logistic logistic)
        {
            if (id != logistic.LogisticId)
            {
                return BadRequest();
            }

            //_context.Entry(logistic).State = EntityState.Modified;
            _unitOfWork.Logistics.Update(logistic);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!LogisticExists(id))
                if (!await LogisticExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Logistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logistic>> PostLogistic(Logistic logistic)
        {
            //_context.Logistics.Add(logistic);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Logistics.Insert(logistic);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetLogistic", new { id = logistic.LogisticId }, logistic);
        }

        // DELETE: api/Logistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogistic(int id)
        {
            //var logistic = await _context.Logistics.FindAsync(id);
            var logistic = await _unitOfWork.Logistics.Get(q => q.LogisticId == id);
            if (logistic == null)
            {
                return NotFound();
            }

            //_context.Logistics.Remove(logistic);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Logistics.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool LogisticExists(int id)
        private async Task<bool> LogisticExists(int id)
        {
            //return _context.Logistics.Any(e => e.LogisticId == id);
            var logistic = await _unitOfWork.Logistics.Get(q => q.LogisticId == id);
            return logistic != null;
        }
    }
}
