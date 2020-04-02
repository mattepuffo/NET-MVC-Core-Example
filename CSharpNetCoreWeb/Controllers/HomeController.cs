using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSharpNetCoreWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CSharpNetCoreWeb.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private IConfiguration configuration { get; }
        private string connectionString = "";

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index(Persona p)
        {
            if (p == null)
            {
                p = new Persona();
            }

            PersonaDb db = new PersonaDb(connectionString);
            ModelState.Clear();
            p.list = db.GetAll();

            return View(p);
        }

        public IActionResult Dettaglio(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            PersonaDb db = new PersonaDb(connectionString);
            ModelState.Clear();
            Persona p = db.GetDetails(id.GetValueOrDefault());

            return View(p);
        }

        public ActionResult Modifica(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            PersonaDb db = new PersonaDb(connectionString);
            ModelState.Clear();
            Persona p = db.GetDetails(id.GetValueOrDefault());

            return View(p);
        }

        [HttpPost]
        public ActionResult ModificaPersona(int id, Persona p)
        {
            if (ModelState.IsValid)
            {
                PersonaDb db = new PersonaDb(connectionString);
                if (db.ModificaPersona(id, p))
                {
                    ViewBag.Message = "Operazione eseguita";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddPersona(Persona p)
        {
            if (ModelState.IsValid)
            {
                PersonaDb db = new PersonaDb(connectionString);
                if (db.AddPersona(p))
                {
                    ViewBag.Message = "Record aggiunto con successo";
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeletePersona(int id)
        {
            if (ModelState.IsValid)
            {
                PersonaDb emprep = new PersonaDb(connectionString);
                if (emprep.DeletePersona(id))
                {
                    ViewBag.Message = "Record Deleted Successfully";
                }
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
