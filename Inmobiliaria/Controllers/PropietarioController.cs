using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private readonly IConfiguration configuration;
        RepositorioPropietario repositorio;

        //constructor para pasar IConfiguration 
            public PropietarioController(IConfiguration conf)
            {
                this.configuration = conf;
                repositorio = new RepositorioPropietario(conf);
            
            }   



            public ActionResult Index()
            {
            var lista = repositorio.ObtenerTodos();
            ViewData[nameof(Propietario)] = lista;
            return View(lista);
            }



        // GET: PropietarioController/Details/5
            public ActionResult Details(int id)
            {

            var propietario = repositorio.Detalles(id);

            return View(propietario);

            }

        // GET: PropietarioController/Create
            public ActionResult Create()
            {
            return View();
            }


        // POST: PropietarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
           public ActionResult Create(Propietario p)
           {
            repositorio.Agregar(p);
            return RedirectToAction(nameof(Index));
           }



        // GET: PropietarioController/Edit/5
            public ActionResult Edit(int id)
            {
            var entidad = repositorio.ObtenerPorId(id);

            return View(entidad);

            }

        // POST: PropietarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, Propietario entidad)
            {

               try
             {
                entidad.Id = id;
                repositorio.Editar(entidad);
                return RedirectToAction(nameof(Index));

             }
              catch (Exception ex)
             {

                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
             }
            }

        // GET: PropietarioController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {

            var propietario = repositorio.Detalles(id);
            return View(propietario);
        }


        // POST: PropietarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Propietario p)
        {
            try
            {
                repositorio.Borrar(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(p);
            }
        }

    }
}
