﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<MemberShipTypeDto, MemberShipType>();
            CreateMap<GenreDto, Genre>();


            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();

        }
    }
}