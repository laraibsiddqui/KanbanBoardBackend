using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Data;
using MyTask.Models;
using System.Threading.Tasks;

namespace MyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public TicketController(ApplicationDbContext db)
        {
            this.db = db;

        }
        [HttpGet]
        public IActionResult GetTicket()
        {
            return Ok(db.ticket.ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTicketById([FromRoute] Guid id)
        {
            var task = db.ticket.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);

        }

        [HttpGet("UserId")]
        public async Task<IActionResult> GetTicketByUserId(string userId)
        {
            var records =  db.ticket.Where(r => r.UserId == userId).ToList();

            if (records == null || records.Count < 0)
            {
                return NotFound(); 
            }

            return Ok(records);

        }

        [HttpPost]

        public IActionResult CreateTicket([FromBody] Ticket addTicket)
        {
            
            addTicket.Id = new System.Guid();
            addTicket.Status = "ToDo";
            db.ticket.Add(addTicket);
            db.SaveChanges();
            return Ok(addTicket);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateTicket([FromRoute] Guid id, [FromBody] Ticket updateTicket)
        {
            var task = await db.ticket.FindAsync(id);
            if (task != null)
            {
                task.UserId=updateTicket.UserId;
                task.Title = updateTicket.Title;
                task.Status= updateTicket.Status;
                await db.SaveChangesAsync();
                return Ok(task);
            }


            return NotFound();

        }

       


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteTicket([FromRoute] Guid id)
        {
            try
            {
                var task = await db.ticket.FindAsync(id);
                if (task != null)
                {
                    db.Remove(task);
                    await db.SaveChangesAsync();
                    return Ok(task);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here or log it
                return StatusCode(500, "An error occurred while deleting the ticket.");
            }
        }

        [HttpPut("TicketStatus")]
        public async Task<IActionResult> UpdateTicketStatus(Guid ticketId, string ticketStatus)
        {
            var ticket = await db.ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = ticketStatus; 
                await db.SaveChangesAsync();
                return Ok(ticket);
            }
            return NotFound();
        }



    }
}
