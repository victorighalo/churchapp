using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using KingdomJoy.Dtos;
using KingdomJoy.Models;

namespace KingdomJoy.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();
        }
    }
}