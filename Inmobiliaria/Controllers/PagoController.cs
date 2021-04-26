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
    public class PagoController : Controller

    {
        protected readonly IConfiguration conf;
        RepositorioPago repositorio;
        RepositorioContrato repositorioContrato;

    

    public PagoController(IConfiguration configuration)
    {
        this.conf = configuration;
        repositorio = new RepositorioPago(configuration);
        repositorioContrato= new RepositorioContrato(configuration);


    }


        // GET: PagoController
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            ViewBag.Pago = lista;
            return View(lista);
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            Pago p = repositorio.ObtenerPorId(id);
            return View(p);
        }



        // GET: PagoController/Create
        public ActionResult Create(int id)
        {
            ViewBag.Contratos = repositorioContrato.ObtenerId(id);
            return View();
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago p)
        {
            try
            {
                repositorio.Agregar(p);

                return RedirectToAction(nameof(Index));
              
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Edit/5
        public ActionResult Edit( int id)
        {
            ///ViewBag.Contratos = repositorioContrato.ObtenerId(idCon);
             //int  nro =repositorio.ObtenerId(id);
            Pago p= repositorio.ObtenerPorId(id);
            return View(p);
        }

        // POST: PagoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {
            try
            {
                pago.Id = id;
                repositorio.Editar(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoController/Delete/5
        public ActionResult Delete(int id)
        {
            Pago c = repositorio.ObtenerPorId(id);
            return View(c);
        }

        // POST: PagoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago p)
        {
            try
            {
                repositorio.Eliminar(id);
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
