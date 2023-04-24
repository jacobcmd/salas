using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salas.Modelos;

namespace salas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {
        private readonly DataContext _context;

        public ReservacionesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reservaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservaciones>>> Getreservaciones()
        {
          if (_context.reservaciones == null)
          {
              return NotFound();
          }
            return await _context.reservaciones.ToListAsync();
        }

        // GET: api/Reservaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservaciones>> GetReservaciones(int id)
        {
          if (_context.reservaciones == null)
          {
              return NotFound();
          }
            var reservaciones = await _context.reservaciones.FindAsync(id);

            if (reservaciones == null)
            {
                return NotFound();
            }

            return reservaciones;
        }

        // PUT: api/Reservaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservaciones(int id, Reservaciones reservaciones)
        {
            if (id != reservaciones.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservaciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionesExists(id))
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

        // POST: api/Reservaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservaciones>> PostReservaciones(Reservaciones reservaciones)
        {
          if (_context.reservaciones == null)
          {
              return Problem("Entity set 'DataContext.reservaciones'  is null.");
          }

            var result = await _context.reservaciones.Select(r => new { r.FechaInicial, r.FechaFinal }).ToListAsync();
            var Reservado = false;

            //se recorre fila x fila de lo que se obtubo de la bd y se usa la funcion comparar para si la fecha Inicial que el usuario quiere ya esta reservada
            foreach (var reserva in result)
            {
                var revision = comprobar(reserva.FechaInicial, reserva.FechaFinal, reservaciones.FechaInicial);
                if (revision)
                {
                    Reservado = true;
                }
            }
            //se recorre fila x fila de lo que se obtubo de la bd y se usa la funcion comparar para si la fecha Inicial que el usuario quiere ya esta reservada
            foreach (var reserva in result)
            {
                var revision = comprobar(reserva.FechaInicial, reserva.FechaFinal, reservaciones.FechaFinal);
                if (revision)
                {
                    Reservado = true;
                }
            }

            if (Reservado == false){
                _context.reservaciones.Add(reservaciones);
                await _context.SaveChangesAsync();
            }
            

            return CreatedAtAction("GetReservaciones", new { id = reservaciones.Id }, reservaciones);
        }

        // DELETE: api/Reservaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaciones(int id)
        {
            if (_context.reservaciones == null)
            {
                return NotFound();
            }
            var reservaciones = await _context.reservaciones.FindAsync(id);
            if (reservaciones == null)
            {
                return NotFound();
            }

            _context.reservaciones.Remove(reservaciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservacionesExists(int id)
        {
            return (_context.reservaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
