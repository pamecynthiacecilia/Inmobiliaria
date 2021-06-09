using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]


    public class ContratosController : ControllerBase
    {

        private readonly DataContext contexto;

        public ContratosController(DataContext contexto)
        {
            this.contexto = contexto;
        }



        // GET: api/<ContratosController>
        [HttpGet("inmueblesConContrato")]
        public async Task<ActionResult> InmueblesConContrato()
        {

            try
            {

                var usuario = User.Identity.Name;
                var contratosVigentes = contexto.Contratos

              .Include(x => x.InquilinoContrato)
              .Include(x => x.InmuebleContrato)
              .Where(c => c.InmuebleContrato.PropietarioInmueble.Email == usuario).ToList();
                // .ThenInclude(x => x.PropietarioInmueble)
                //.Select(x => new{ x.InmuebleContrato.Direccion, x.InmuebleContrato.Imagen }).ToList();

                return Ok(contratosVigentes);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("obtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                var contratoPorId = contexto.Contratos
                .Include(x => x.InquilinoContrato)
                .Include(x => x.InmuebleContrato)
                 .Where(c => c.InmuebleContrato.PropietarioInmueble.Email == usuario)
                 .Single(e => e.Id == id);
                return Ok(contratoPorId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }   
}
