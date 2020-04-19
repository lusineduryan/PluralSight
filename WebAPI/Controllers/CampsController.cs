using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreCodeCamp.Data;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly CampContext _context;

        public CampsController(CampContext context)
        {
            _context = context;

        }

        // GET: api/Camps
        [HttpGet]
        public IEnumerable<Camp> GetCamps()
        {
            return _context.Camps;
        }

        // GET: api/Camps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCamp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var camp = await _context.Camps.FindAsync(id);

            if (camp == null)
            {
                return NotFound();
            }

            return Ok(camp);
        }

        // PUT: api/Camps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamp([FromRoute] int id, [FromBody] Camp camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != camp.CampId)
            {
                return BadRequest();
            }

            _context.Entry(camp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampExists(id))
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

        // POST: api/Camps
        [HttpPost]
        public async Task<IActionResult> PostCamp([FromBody] Camp camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Camps.Add(camp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamp", new { id = camp.CampId }, camp);
        }

        // DELETE: api/Camps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var camp = await _context.Camps.FindAsync(id);
            if (camp == null)
            {
                return NotFound();
            }

            _context.Camps.Remove(camp);
            await _context.SaveChangesAsync();

            return Ok(camp);
        }

        private bool CampExists(int id)
        {
            return _context.Camps.Any(e => e.CampId == id);
        }
    }
}