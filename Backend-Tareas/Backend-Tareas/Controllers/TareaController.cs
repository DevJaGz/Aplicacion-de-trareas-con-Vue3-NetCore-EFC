using AutoMapper;
using Backend_Tareas.Context;
using Backend_Tareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Tareas.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TareaController : ControllerBase
   {

      private readonly IMapper _mapper;
      private readonly AppDbContext _context;

      public TareaController(AppDbContext context, IMapper mapper)
      {
         _mapper = mapper;
         _context = context;
      }


      [HttpGet]
      public async Task<IActionResult> Get()
      {
         try
         {
            var listaTarea = await _context.Tareas.ToListAsync();
            return Ok(listaTarea);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }

      }

      [HttpPost]
      public async Task<IActionResult> Post([FromBody] RequestTarea requestTarea)
      {
         try
         {
            Tarea tarea = _mapper.Map<Tarea>(requestTarea);
            tarea.Id = 0;
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "¡La tarea fue registrada con éxito!", tarea });
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         try
         {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
               return NotFound();
            }
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return Ok(new { msg = $"Tarea con id {id} Eliminada" });
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }

      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] RequestTarea requestTarea)
      {
         try
         {
            Tarea tarea = _mapper.Map<Tarea>(requestTarea);
            if (id != tarea.Id)
            {
               return BadRequest("El Id en el Enpoint no coincide con el del Body");
            }
            tarea.Estado = !tarea.Estado;
            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { msg = "¡La tarea fue Actualizada con éxito!", tarea = tarea });
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }


      //  BACKEND OK:

      //private readonly AppDbContext _context;

      //public TareaController(AppDbContext context)
      //{
      //   _context = context;

      //}

      //[HttpGet]
      //public async Task<IActionResult> Get()
      //{
      //   try
      //   {
      //      var listaTarea = await _context.Tareas.ToListAsync();
      //      return Ok(listaTarea);
      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }

      //}

      //[HttpPost]
      //public async Task<IActionResult> Post([FromBody] Tarea tarea)
      //{
      //   try
      //   {
      //      _context.Tareas.Add(tarea);
      //      await _context.SaveChangesAsync();
      //      return Ok(new { msg = "¡La tarea fue registrada con éxito!", tarea = tarea});
      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }
      //}

      //[HttpPut("{id}")]
      //public async Task<IActionResult> Put(int id, [FromBody] Tarea tarea)
      //{
      //   try
      //   {
      //      if(id != tarea.Id)
      //      {
      //         return BadRequest("El Id en el Enpoint no coincide con el del Body");
      //      }
      //      tarea.Estado = !tarea.Estado;
      //      _context.Entry(tarea).State = EntityState.Modified;
      //      await _context.SaveChangesAsync();
      //      return Ok(new { msg = "¡La tarea fue Actualizada con éxito!", tarea = tarea });
      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }
      //}

      //[HttpDelete("{id}")]
      //public async Task<IActionResult> Delete(int id)
      //{
      //   try
      //   {
      //      var tarea = await _context.Tareas.FindAsync(id);
      //      if (tarea == null)
      //      {
      //         return NotFound();
      //      }
      //      _context.Tareas.Remove(tarea);
      //      await _context.SaveChangesAsync();
      //      return Ok(new { msg = "Tarea Eliminada" });
      //   }
      //   catch (Exception ex)
      //   {
      //      return BadRequest(ex.Message);
      //   }

      //}


   }
}
