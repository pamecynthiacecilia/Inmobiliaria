using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inmobiliaria.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]

    public class PropietariosController : ControllerBase
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public PropietariosController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }


        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<Propietario>> Get()
        {
            try
            {
               
                var usuario = User.Identity.Name;
                return contexto.Propietarios.SingleOrDefault(x => x.Email == usuario);
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
                return Ok(contexto.Propietarios.SingleOrDefault(x => x.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(contexto.Propietarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<controller>/5
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginView loginView)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginView.Clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                var p = contexto.Propietarios.FirstOrDefault(x => x.Email == loginView.Usuario);
                if (p == null || p.Clave != hashed)
                {
                    return BadRequest("Nombre de usuario o clave incorrecta");
                }
                else
                {
                    var key = new SymmetricSecurityKey(
                        System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
                    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, p.Email),
                        new Claim("FullName", p.Nombre + " " + p.Apellido),
                        new Claim(ClaimTypes.Role, "Propietario"),
                    };

                    var token = new JwtSecurityToken(
                        issuer: config["TokenAuthentication:Issuer"],
                        audience: config["TokenAuthentication:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credenciales
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet ("usuarioActual")]
        public async Task<ActionResult<Propietario>> UsuarioActual()
        {
            try
            {
               
                var usuario = User.Identity.Name;
               
                return Ok(contexto.Propietarios.SingleOrDefault(x => x.Email == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }




        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Propietario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Propietarios.Add(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.Id }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<controller>
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, Propietario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var usuario = User.Identity.Name;
                    Propietario usuarioActual = contexto.Propietarios.SingleOrDefault(x => x.Email == usuario);

                    if (entidad.Dni != "" && entidad.Dni != null) {usuarioActual.Dni = entidad.Dni; }
                    if (entidad.Nombre != "" && entidad.Nombre != null) { usuarioActual.Nombre = entidad.Nombre; }
                    if (entidad.Apellido != "" && entidad.Apellido != null) { usuarioActual.Apellido = entidad.Apellido; }
                    if (entidad.Tel != "" && entidad.Tel != null) { usuarioActual.Tel = entidad.Tel; }
                    if (entidad.Email != "" && entidad.Email != null) { usuarioActual.Email = entidad.Email; }
                    if (entidad.Clave != "" && entidad.Clave != null) { usuarioActual.Clave = entidad.Clave; }


                    contexto.Propietarios.Update(usuarioActual);
                    contexto.SaveChanges();
                    return Ok(usuarioActual);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // PUT api/<controller>
        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] Propietario entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var usuario = User.Identity.Name;
                    Propietario usuarioActual = contexto.Propietarios.SingleOrDefault(x => x.Email == usuario);

                    if (entidad.Dni != "" && entidad.Dni != null) { usuarioActual.Dni = entidad.Dni; }
                    if (entidad.Nombre != "" && entidad.Nombre != null) { usuarioActual.Nombre = entidad.Nombre; }
                    if (entidad.Apellido != "" && entidad.Apellido != null) { usuarioActual.Apellido = entidad.Apellido; }
                    if (entidad.Tel != "" && entidad.Tel != null) { usuarioActual.Tel = entidad.Tel; }
                    if (entidad.Email != "" && entidad.Email != null) { usuarioActual.Email = entidad.Email; }
                    if (entidad.Clave != "" && entidad.Clave != null) { usuarioActual.Clave = entidad.Clave; }


                    contexto.Propietarios.Update(usuarioActual);
                    contexto.SaveChanges();
                    return Ok(usuarioActual);
                }
                return BadRequest();
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
                if (ModelState.IsValid)
                {
                    var p = contexto.Propietarios.Find(id);
                    if (p == null)
                        return NotFound();
                    contexto.Propietarios.Remove(p);
                    contexto.SaveChanges();
                    return Ok(p);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /*GET: /salir
        [Route("salir", Name = "logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction();
        }*/


        // GET: api/<controller>
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            try
            {
                return Ok("anduvo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<controller>
        [HttpGet("test/{codigo}")]
        [AllowAnonymous]
        public IActionResult Code(int codigo)
        {
            try
            {
                //StatusCodes.Status418ImATeapot //constantes con códigos
                return StatusCode(codigo, new { Mensaje = "Anduvo", Error = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
