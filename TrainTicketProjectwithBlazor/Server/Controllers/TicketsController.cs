using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTicketProjectwithBlazor.Shared.DTO;
using TrainTicketProjectwithBlazor.Shared.Models;

namespace TrainTicketProjectwithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TrainDbContext _context;

        public TicketsController(TrainDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            return await _context.Tickets.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<TicketViewDTO>>> GetTicketDTOs()
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            return await _context.Tickets
                .Include(o => o.Passenger)
                .Include(o => o.TicketItems).ThenInclude(oi => oi.Train)
                .Select(o =>
                    new TicketViewDTO
                    {
                        TicketId = o.TicketId,
                        FromStation = o.FromStation,
                        ToStation = o.ToStation,
                        JourneyDate = o.JourneyDate,
                        PassengerName = o.Passenger.PassengerName,
                        Category = o.Category,
                        ItemCount = o.TicketItems.Count,
                        TicketValue = o.TicketItems.Sum(x => x.Train.TicketPrice * x.Quantity)
                    })
                .ToListAsync();
        }
        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<TicketViewDTO>> GetTicketViewDTO(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.Include(o => o.Passenger)
                .Include(o => o.TicketItems).ThenInclude(oi => oi.Train).FirstOrDefaultAsync(o => o.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return new TicketViewDTO
            {
                TicketId = ticket.TicketId,
                FromStation = ticket.FromStation,
                ToStation = ticket.ToStation,
                JourneyDate = ticket.JourneyDate,
                PassengerName = ticket.Passenger.PassengerName,
                Category = ticket.Category,
                ItemCount = ticket.TicketItems.Count,
                TicketValue = ticket.TicketItems.Sum(x => x.Train.TicketPrice * x.Quantity)

            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<TicketItemViewDTO>>> GetTicketItems(int id)
        {
            if (_context.TicketItems == null)
            {
                return NotFound();
            }
            var ticketitems = await _context.TicketItems.Include(x => x.Train).Where(oi => oi.TicketId == id).ToListAsync();

            if (ticketitems == null)
            {
                return NotFound();
            }

            return ticketitems.Select(oi => new TicketItemViewDTO { TicketId = oi.TicketId, TrainName = oi.Train.TrainName, TicketPrice = oi.Train.TicketPrice, Quantity = oi.Quantity }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<TicketItem>>> GetTicketItemsOf(int id)
        {
            if (_context.TicketItems == null)
            {
                return NotFound();
            }
            var ticketitems = await _context.TicketItems.Where(oi => oi.TicketId == id).ToListAsync();

            if (ticketitems == null)
            {
                return NotFound();
            }

            return ticketitems;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutTicketWithTicketItem(int id, TicketEditDTO ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }
            var existing = await _context.Tickets.Include(x => x.TicketItems).FirstAsync(o => o.TicketId == id);
            _context.TicketItems.RemoveRange(existing.TicketItems);
            existing.TicketId = ticket.TicketId;
            existing.FromStation = ticket.FromStation;
            existing.ToStation = ticket.ToStation;
            existing.JourneyDate = ticket.JourneyDate;
            existing.PassengerId = ticket.PassengerId;
            existing.Category = ticket.Category;
            foreach (var item in ticket.TicketItems)
            {
                _context.TicketItems.Add(new TicketItem { TicketId = ticket.TicketId, TrainId = item.TrainId, Quantity = item.Quantity });
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);

            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'TrainDbContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }
        [HttpPost("DTO")]
        public async Task<ActionResult<Ticket>> PostTicketDTO(TicketDTO dto)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'TrainDbContext.Tickets'  is null.");
            }
            var ticket = new Ticket { PassengerId = dto.PassengerId, FromStation = dto.FromStation, ToStation = dto.ToStation, JourneyDate = dto.JourneyDate, Category = dto.Category };
            foreach (var oi in dto.TicketItems)
            {
                ticket.TicketItems.Add(new TicketItem { TrainId = oi.TrainId, Quantity = oi.Quantity });
            }
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
