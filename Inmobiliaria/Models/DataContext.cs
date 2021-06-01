using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class DataContext : DbContext
    {
public DataContext(DbContextOptions<DataContext>options): base(options)
        {

        }

        //tablas a las que voy a hacer consultas 
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
    }
}
