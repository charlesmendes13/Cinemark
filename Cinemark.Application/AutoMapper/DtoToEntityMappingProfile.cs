﻿using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Domain.Models;

namespace Cinemark.Application.AutoMapper
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<GetFilmeByIdDto, Filme>();
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<DeleteFilmeDto, Filme>();
        }
    }
}
