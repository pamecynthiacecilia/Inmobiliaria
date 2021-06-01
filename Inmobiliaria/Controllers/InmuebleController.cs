using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class InmuebleController : Controller
    {

        protected readonly IConfiguration conf;
        RepositorioInmueble repositorio;
        RepositorioPropietario repositorioPropietario;
        private readonly IWebHostEnvironment environment;




        public InmuebleController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.conf = configuration;
            repositorio = new RepositorioInmueble(configuration);
            this.environment = environment;
            repositorioPropietario = new RepositorioPropietario(configuration);


        }

        // GET: InmuebleController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();

            ViewData[nameof(Inmueble)] = lista;

            return View(lista);
        }

        public ActionResult Disponibles()
        {
            var lista = repositorio.ObtenerDisponible();

            ViewData[nameof(Inmueble)] = lista;

            return View(lista);
        }


        public ActionResult InmueblesPorPropietario(int id)
        {
            var lista = repositorio.inmueblesPorPropietario(id);

            ViewData[nameof(Inmueble)] = lista;

            return View(lista);
        }


        // GET: InmuebleController/Details/5
        public ActionResult Details(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        // GET: InmuebleController/Create
        public ActionResult Create()
        {
            ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
            return View();
        }

        // POST: InmuebleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble e)
        {

            repositorio.Agregar(e);
            if (e.ImageFile != null)
            {
                string wwwPath = environment.WebRootPath;
                string path = Path.Combine(wwwPath, "propiedades");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
                string fileName = "propiedad_" + e.Id + Path.GetExtension(e.ImageFile.FileName);
                string pathCompleto = Path.Combine(path, fileName);
                e.Imagen = Path.Combine("/propiedades", fileName);
                using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                {
                    e.ImageFile.CopyTo(stream);
                }

                repositorio.Editar(e);
            }
            return RedirectToAction(nameof(Index));
        }



        // GET: InmuebleController/Edit/5
       
        public ActionResult Edit(int id)
        {
          Inmueble e = repositorio.ObtenerPorId(id);
            ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();

            return View(e);
            
        }

        // POST: InmuebleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble e)
        {
            try
            {
                e.Id = id;
                repositorio.Editar(e);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerTodos();
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(e);
            }
        }



        // GET: InmuebleController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
           /* if (TempData.ContainsKey("Mensaje"))
              ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];*/
            return View(entidad);
        }

        // POST: InmuebleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Inmueble entidad)
        {
            try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }
        }
    }
}
