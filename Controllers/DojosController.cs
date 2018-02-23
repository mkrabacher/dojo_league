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
    public class DojosController : Controller
    {
        private MyContext _context;

        public DojosController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dojos")]
        public IActionResult ShowDojos()
        {
            ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
            ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
            return View("Dojos");
        }

        [HttpPost]
        [Route("NewDojo")]
        public IActionResult NewDojo(Dojo model)
        {
            if(ModelState.IsValid)
            {
                Dojo NewDojo = new Dojo()
                {
                    Name = model.Name,
                    Location = model.Location,
                    Description = model.Description,
                };
                _context.Add(NewDojo);
                _context.SaveChanges();
                
                ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
                ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
                return Redirect("/Dojos");
            }
            ViewBag.Ninjas = _context.Ninjas.Include(nin => nin.Dojo).ToList();
            ViewBag.Dojos = _context.Dojos.Include(doj => doj.Ninjas).ToList();
            return View("Dojos", model);
        }

        [HttpGet]
        [Route("Dojos/{id}")]
        public IActionResult DojoPage(int id)
        {
            ViewBag.MemberNinjas = _context.Ninjas.Include("Dojo").Where(nin => nin.Dojo.Id == id).ToList();
            ViewBag.RoninNinjas = _context.Ninjas.Include("Dojo").Where(nin => nin.Dojo.Id == 0).ToList();
            Dojo dojo = _context.Dojos.Where(doj => doj.Id == id).Include("Ninjas").ToList()[0];
            return View("DojoPage", dojo);
        }

        [HttpGet]
        [Route("Recruit/{DojoId}/{NinjaId}")]
        public IActionResult Recruit(int DojoId, int NinjaId)
        {
            Ninja ninja = _context.Ninjas.SingleOrDefault(nin => nin.Id == NinjaId);
            ninja.DojoId = DojoId;
            _context.SaveChanges();
            return RedirectToAction("DojoPage", new {id = DojoId});
        }

        [HttpGet]
        [Route("Banish/{DojoId}/{NinjaId}")]
        public IActionResult Banish(int DojoId, int NinjaId)
        {
            Ninja ninja = _context.Ninjas.SingleOrDefault(nin => nin.Id == NinjaId);
            ninja.DojoId = 0;
            _context.SaveChanges();
            return RedirectToAction("DojoPage", new {id = DojoId});
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
