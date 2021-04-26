using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioBase
    {
        protected readonly IConfiguration conf;
        protected readonly string connectionString;

        protected RepositorioBase(IConfiguration configuration)
        {
            this.conf = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];

        }

    }
}
