using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InmueblesController : Controller
    {
        private readonly DataContext contexto;

        public InmueblesController(DataContext contexto)
        {
            this.contexto = contexto;
        }



        // GET: api/<controller>
        [HttpGet("inmueblesDelPropietario")]
        public async Task<ActionResult> InmueblesDelPropietario()
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmuebles.Include(x=>x.PropietarioInmueble).Where(e => e.PropietarioInmueble.Email == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        // GET: api/<controller>
        [HttpGet("inmueblesDisponiblesDelPropietario")]
        public async Task<ActionResult> InmueblesDisponiblesDelPropietario()
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmuebles.Where(e => e.Disponible == 1 && e.PropietarioInmueble.Email == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpGet("obtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmuebles.Include(e => e.PropietarioInmueble).Where(e => e.PropietarioInmueble.Email == usuario).Single(e => e.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //Me sirve para modificar todos los campos
        [HttpPut("modificarDisponible")]
        public async Task<IActionResult> ModificarDisponible(int id, [FromBody] Inmueble entidad)
        {
            try
            {
                var usuario = User.Identity.Name;
                id = entidad.Id;


                var control = contexto.Inmuebles.Include(i => i.PropietarioInmueble.Email)
                    .Where(x => x.Id == id && x.PropietarioInmueble.Email == usuario);


                if (ModelState.IsValid && control != null)
                {

                    if (entidad.Disponible == 1)
                    {
                        entidad.Disponible = 2;
                        // contexto.Attach(entidad);
                        // contexto.Entry(entidad).Property("disponible").IsModified== true;
                        contexto.Inmuebles
                        .Update(entidad).Property("Disponible");

                    }

                    else
                    {
                        entidad.Disponible = 1;
                        contexto.Inmuebles
                        .Update(entidad).Property("Disponible");
                    }

                    contexto.SaveChanges();

                }
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        // PUT api/<controller>

        [HttpPut]
        public async Task<IActionResult> Put(Inmueble inmueble)
        {
            try
            {

                    contexto.Update(inmueble.Disponible);
                    contexto.SaveChanges();
                    return Ok(inmueble);  
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmuebles.Include(e => e.PropietarioInmueble).Where(e => e.PropietarioInmueble.Email == usuario).Single(e => e.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST Crear Inmueble
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.PropietarioId = contexto.Propietarios.Single(e => e.Email == User.Identity.Name).Id;
                    contexto.Inmuebles.Add(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task<IActionResult> Put(int id, Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Inmuebles.AsNoTracking().Include(e=>e.PropietarioInmueble).FirstOrDefault(e => e.Id == id && e.PropietarioInmueble.Email == User.Identity.Name) != null)
                {
                    entidad.Id = id;
                    contexto.Inmuebles.Update(entidad).Property("Disponible");
                    contexto.SaveChanges();
                    
                }
               return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = contexto.Inmuebles.Include(e => e.PropietarioInmueble).FirstOrDefault(e => e.Id == id && e.PropietarioInmueble.Email == User.Identity.Name);
                if (entidad != null)
                {
                    contexto.Inmuebles.Remove(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
		}

        // DELETE api/<controller>/5
        /*	[HttpDelete("BajaLogica/{id}")]
            public async Task<IActionResult> BajaLogica(int id)
            {
                try
                {
                    var entidad = contexto.Inmuebles.Include(e => e.PropietarioInmueble).FirstOrDefault(e => e.Id == id && e.PropietarioInmueble.Email == User.Identity.Name);
                    if (entidad != null)
                    {
                        entidad.Superficie = -1;//cambiar por estado = 0
                        contexto.Inmuebles.Update(entidad);
                        contexto.SaveChanges();
                        return Ok();
                    }
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }*/
    }
}
