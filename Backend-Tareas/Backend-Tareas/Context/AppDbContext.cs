using Backend_Tareas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Tareas.Context
{
   public class AppDbContext: DbContext // Permite crear una sesión de la db  y de esta manera acceder a los datos y guardar instancias de los modelos
   {

      public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
      {

      }
      public DbSet<Tarea> Tareas {  get; set; }   // Binding del modelo. 'Tareas' Nombre de la tabla

   }
}
