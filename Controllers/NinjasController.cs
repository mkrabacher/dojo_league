using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dojo_league.Models;

namespace dojo_league.Controllers
{
    public class NinjasController : Controller
    {
        private MyContext _context;

        public NinjasController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("ShowNinjas");
        } 

        [HttpGet]
        [Route("Ninjas")]
        public IActionResult ShowNinjas()
        {
            ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
            ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
            return View("Ninjas");
        }

        [HttpPost]
        [Route("NewNinja")]
        public IActionResult NewNinja(Ninja model)
        {
            if(ModelState.IsValid)
            {
                Ninja NewNinja = new Ninja()
                {
                    Name = model.Name,
                    Level = model.Level,
                    DojoId = model.DojoId,
                    Description = model.Description,
                };
                _context.Add(NewNinja);
                _context.SaveChanges();
                
                ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
                ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
                return Redirect("/Ninjas");
            }
            ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
            ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
            return View("Ninjas", model);
        }

        [HttpGet]
        [Route("Ninjas/{id}")]
        public IActionResult NinjaPage(int id)
        {
            Ninja ninja = _context.Ninjas.Where(nin => nin.Id == id).Include("Dojo").ToList()[0];
            return View("NinjaPage", ninja);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
