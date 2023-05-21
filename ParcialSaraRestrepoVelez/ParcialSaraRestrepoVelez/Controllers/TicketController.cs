﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ParcialSaraRestrepoVelez.DAL;
using ParcialSaraRestrepoVelez.DAL.Entities;
using System.Data.Entity;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace ParcialSaraRestrepoVelez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly DataBaseContext _context;

        public TicketController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            var ticket = _context.Tickets.ToList(); 

            if (ticket == null) return NotFound();

            return ticket;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? id)
        {
            var ticket = _context.Tickets.FirstOrDefault(c => c.Id == id);

            if (ticket == null) return NotFound();

            return Ok(ticket);
        }


        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                ticket.Id = Guid.NewGuid();
                ticket.CreatedDate = DateTime.Now;
                ticket.IsUsed = false;

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", ticket.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }


        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditTicket(Guid? id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id) return NotFound("Boleta no válida");

                ticket.ModifiedDate = DateTime.Now;
                ticket.UseDate = DateTime.Now;

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", ticket.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteTicket(Guid? id)
        {
            if (_context.Tickets == null) return Problem("Entity set 'DataBaseContext.Tickets' is null.");
            var ticket = _context.Tickets.FirstOrDefault(c => c.Id == id);

            if (ticket == null) return NotFound("Category not found");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(); //Hace las veces del Delete en SQL

            return Ok(String.Format("El ticket {0} fue eliminado!", ticket.Id));
        }
    }
}
