using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VentaGalaxy.Entities;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;
using VentaGalaxy.Entities.Infos;

namespace VentaGalaxy.Services.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {

            CreateMap<Producto, ProductoDtoResponse>();

            CreateMap<ProductoInfo, ProductoDtoResponse>();

            CreateMap<ProductoDtoRequest, Producto>()
                .ReverseMap();

            CreateMap<ProductoHomeInfo, ProductoHomeDtoResponse>();



        }
    }
}
