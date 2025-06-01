using AdviLaw.Application.Specializations.Command.CreateSpecialization;
using AdviLaw.Domain.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Specializations.Dtos
{
    class SpecializationProfile:Profile
    {

        public SpecializationProfile()
        {

            CreateMap<CreateSpeciallizationCommand, Specialization>();


            CreateMap<Specialization, SpecializationDto>();

            //.fromMember(s=>s.City,opt=>opt.MapFrom(src=>src.Address==null ?null :src.Address.City))
           // .formMember(d=>d.Postral,opt=>opt.MapFrom(src=>src.Address==null? null:src.Address.Postal))
        }
    }
}
