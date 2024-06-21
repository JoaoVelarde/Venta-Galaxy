using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDtoResponse>();
            CreateMap<ClienteInfo, ClienteDtoResponse>();
            CreateMap<Cliente, ClienteDtoRequest>()
                .ReverseMap();

        }
    }
}
