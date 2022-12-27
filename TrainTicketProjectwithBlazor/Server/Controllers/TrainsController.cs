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
    public class TrainsController : ControllerBase
    {
        private readonly TrainDbContext _context;
        private readonly IWebHostEnvironment env;
        public TrainsController(TrainDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Train>>> GetTrains()
        {
          if (_context.Trains == null)
          {
              return NotFound();
          }
            return await _context.Trains.ToListAsync();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrain(int id)
        {
          if (_context.Trains == null)
          {
              return NotFound();
          }
            var train = await _context.Trains.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Train>> PutTrain(int id, Train train)
        {
            if (id != train.TrainId)
            {
                return BadRequest();
            }

            _context.Entry(train).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return train;
        }

        // POST: api/Trains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Train>> PostTrain(Train train)
        {
          if (_context.Trains == null)
          {
              return Problem("Entity set 'TrainDbContext.Trains'  is null.");
          }
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrain", new { id = train.TrainId }, train);
        }
        [HttpPost("Upload")]
        public async Task<ImageUploadResponse> Upload(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            var randomFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var storedFileName = randomFileName + ext;
            using FileStream fs = new FileStream(Path.Combine(env.WebRootPath, "Uploads", storedFileName), FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return new ImageUploadResponse { FileName = file.FileName, StoredFileName = storedFileName };
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            if (_context.Trains == null)
            {
                return NotFound();
            }
            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainExists(int id)
        {
            return (_context.Trains?.Any(e => e.TrainId == id)).GetValueOrDefault();
        }
    }
}
