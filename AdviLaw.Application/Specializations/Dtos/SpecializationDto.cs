using AdviLaw.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Specializations.Dtos
{
  public  class SpecializationDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        //public static SpecializationDto? FromEntity(Specialization? specialization)
        //{

        //    if (specialization == null) return null;


        //    return new SpecializationDto()
        //    {
        //        Id = specialization.Id,
        //        Name = specialization.Name
        //    };
        //}
    }
}
