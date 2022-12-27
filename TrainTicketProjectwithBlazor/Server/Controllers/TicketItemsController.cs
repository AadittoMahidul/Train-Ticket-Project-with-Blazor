using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTicketProjectwithBlazor.Shared.Models;

namespace TrainTicketProjectwithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketItemsController : ControllerBase
    {
        private readonly TrainDbContext _context;

        public TicketItemsController(TrainDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketItem>>> GetTicketItems()
        {
          if (_context.TicketItems == null)
          {
              return NotFound();
          }
            return await _context.TicketItems.ToListAsync();
        }

        // GET: api/TicketItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketItem>> GetTicketItem(int id)
        {
          if (_context.TicketItems == null)
          {
              return NotFound();
          }
            var ticketItem = await _context.TicketItems.FindAsync(id);

            if (ticketItem == null)
            {
                return NotFound();
            }

            return ticketItem;
        }

        // PUT: api/TicketItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketItem(int id, TicketItem ticketItem)
        {
            if (id != ticketItem.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticketItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketItemExists(id))
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

        // POST: api/TicketItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketItem>> PostTicketItem(TicketItem ticketItem)
        {
          if (_context.TicketItems == null)
          {
              return Problem("Entity set 'TrainDbContext.TicketItems'  is null.");
          }
            _context.TicketItems.Add(ticketItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketItemExists(ticketItem.TicketId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketItem", new { id = ticketItem.TicketId }, ticketItem);
        }

        // DELETE: api/TicketItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketItem(int id)
        {
            if (_context.TicketItems == null)
            {
                return NotFound();
            }
            var ticketItem = await _context.TicketItems.FindAsync(id);
            if (ticketItem == null)
            {
                return NotFound();
            }

            _context.TicketItems.Remove(ticketItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketItemExists(int id)
        {
            return (_context.TicketItems?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
