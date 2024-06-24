using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using contactForm.Entities;
using contactForm.Models.CommonModel;

namespace contactForm.Controllers
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            // RegisterModel -> RegisterEntities
            CreateMap<RegisterModel, RegisterEntities>();
        }
    }

}