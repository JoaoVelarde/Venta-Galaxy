﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.DataAccess
{
    public class ClienteIdentityUser : IdentityUser
    {
        [StringLength(100)]
        public string NombreCompleto { get; set; } = default!;
    }
}
