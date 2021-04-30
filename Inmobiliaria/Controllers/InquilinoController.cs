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
    public class InquilinoController : Controller
    {
        protected readonly IConfiguration conf;
        RepositorioInquilino repositorioInquilino;


        public InquilinoController(IConfiguration configuration)
        {
            this.conf = configuration;
            repositorioInquilino = new RepositorioInquilino(configuration);

        }

        // GET: InquilinoController
        public ActionResult Index()
        {
            var lista = repositorioInquilino.ObtenerTodos();
            // ViewBag.Inquilino = lista;
            //ViewData["Inquilino"]=lista;
            ViewData[nameof(Inquilino)] = lista;

            return View();
        }



        // GET: InquilinoController/Details/5
        public ActionResult Details(int id)
        {
            var inquilino = repositorioInquilino.ObtenerPorId(id);

            return View(inquilino);
        }


        // GET: InquilinoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            repositorioInquilino.Agregar(inquilino);
            return RedirectToAction(nameof(Index));

        }


        // GET: InquilinoController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var inquilino = repositorioInquilino.ObtenerPorId(id);
         
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];
            return View(inquilino);

        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy ="Administrador")]
        public ActionResult Delete(int id, Inquilino e)
        {
            try
            {
                repositorioInquilino.Borrar(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(e);
            }
        }


        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int id)
        {

            var entidad = repositorioInquilino.ObtenerPorId(id);
            return View(entidad);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino entidad)
        {

            try
            {
                entidad.Id = id;
                repositorioInquilino.Editar(entidad);

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

    

