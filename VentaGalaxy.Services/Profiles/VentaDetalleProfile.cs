using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VentaGalaxy.Entities.Infos;
using VentaGalaxy.Entities;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Profiles
{
    public class VentaDetalleProfile : Profile
    {
        public VentaDetalleProfile()
        {

            CreateMap<VentaDetalleInfo, VentaDetalleResponse>();
            CreateMap<VentaDetalle, VentaDetalleResponse>();

        }
    }
}
