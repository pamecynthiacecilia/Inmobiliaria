using Inmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    public class InmuebleController : Controller
    {

        protected readonly IConfiguration conf;
        RepositorioInmueble repositorio;
        RepositorioPropietario repositorioPropietario;



        public InmuebleController(IConfiguration configuration)
        {
            this.conf = configuration;
            repositorio = new RepositorioInmueble(configuration);
            repositorioPropietario = new RepositorioPropietario(configuration);


        }

        // GET: InmuebleController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            // ViewBag.Inquilino = lista;
            //ViewData["Inquilino"]=lista;
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
