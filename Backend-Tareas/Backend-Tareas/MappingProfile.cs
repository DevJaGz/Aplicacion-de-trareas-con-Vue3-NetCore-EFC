using AutoMapper;
using Backend_Tareas.Context;
using Backend_Tareas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Tareas
{
   public class MappingProfile : Profile
   {

      public MappingProfile()
      {

         var map = CreateMap<RequestTarea, Tarea>();
         map.ForAllOtherMembers(opt => opt.Ignore());
         map.ForMember(
               destiny => destiny.Nombre,
               origin => origin.MapFrom(source => source.nombre)
            );

         map.ForMember(
               destiny => destiny.Id,
               origin => origin.MapFrom(source => source.id)
            );

         map.ForMember(
               destiny => destiny.Estado,
               origin => origin.MapFrom(source => source.estado)
            );
      }
   }
}
