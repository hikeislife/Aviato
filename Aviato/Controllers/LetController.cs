using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize]
    public class LetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());


        // GET: Let
        [Authorize(Roles = "Admin, SuperUser")]
        public async Task<ActionResult> Index()
        {
            var let = db.Let.Include(l => l.Avion1).Include(l => l.Destinacija1).Include(l => l.Zaposleni).Include(l => l.Zaposleni1).Include(l => l.Zaposleni2).Include(l => l.Zaposleni3).Where(l => l.VremePoletanja >= DateTime.Now).OrderBy(l => l.VremePoletanja);
            return View(await let.ToListAsync());
        }

        //PROSLI LETOVI
        [Authorize(Roles = "Admin, SuperUser")]
        public async Task<ActionResult> Prosliletovi()
        {
            var let = db.Let.Include(l => l.Avion1).Include(l => l.Destinacija1).Include(l => l.Zaposleni).Include(l => l.Zaposleni1).Include(l => l.Zaposleni2).Include(l => l.Zaposleni3)
                            .Where(l => l.VremePoletanja < DateTime.Now).OrderBy(l => l.VremePoletanja);
            return View(await let.ToListAsync());
        }

        //LETOVI SA IZBRISANIM CLANOM POSADE
        [Authorize(Roles = "Admin, SuperUser")]
        public ActionResult LetoviBezDelaPosade()
        {
            List<Let> let = db.Let.Where(l => l.NepotpunaPosada == 1 && l.VremePoletanja >= DateTime.Now).Select(l => l).ToList();

            return View(let);
        }

        // GET: Let/Details/5
        [Authorize(Roles = "Admin, SuperUser")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Let let = await db.Let.FindAsync(id);
            if (let == null)
            {
                return HttpNotFound();
            }
            return View(let);
        }

        // GET: Let/Create
        [Authorize(Roles = "Admin, SuperUser")]
        public async Task<ActionResult> Create(int? id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var role = await roleManager.FindByIdAsync(user.Roles.FirstOrDefault().RoleId);

            List<Zaposleni> piloti = db.Pilot.Where(p => p.OcenaZS == true)
                              .Join(db.Zaposleni, p => p.SifraPilota, z => z.ZaposleniId, (p, z) => z).ToList();

            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");

            ViewBag.Avion = new SelectList(db.Avion.Where(a => a.ServisniStatus == false), "AvionId", "SifraAviona");
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv");
            ViewBag.Kopilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme");
            ViewBag.Pilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme");
            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme");
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme");

            return View();
        }

        // POST: Let/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LetId,Destinacija,Avion,VremePoletanja,Pilot,Kopilot,Stjuard1,Stjuard2")] Let let)
        {
            if (ModelState.IsValid)
            {
                if (let.Stjuard1 == null || let.Stjuard2 == null || let.Pilot == null || let.Kopilot == null)
                {
                    ViewBag.Greska = "Let mora imati celokupnu posadu";
                }
                else if (let.Stjuard1 == let.Stjuard2)
                {
                    ViewBag.Greska = "Stjuardi moraju biti različiti";
                }
                else if (let.Pilot == let.Kopilot)
                {
                    ViewBag.Greska = "Pilot i kopilot moraju biti različiti";
                }
                else
                {
                    try
                    {
                        db.Let.Add(let);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            List<Zaposleni> piloti = db.Pilot.Where(p => p.OcenaZS == true)
                             .Join(db.Zaposleni, p => p.SifraPilota, z => z.ZaposleniId, (p, z) => z).ToList();
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");

            ViewBag.Avion = new SelectList(db.Avion.Where(a => a.ServisniStatus == false), "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Pilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard1 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard2 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");

            return View(let);
        }

        //STJUARDI PO DESTINACIJI
        public ActionResult StjuardiPoDestinaciji(int? id)
        {
            //var stj1 = "";
            //var stj2 = "";

            //if (letId != null)
            //{
            //    stj1 = db.Let.Where(l => l.LetId == letId)
            //                    .Join(db.Zaposleni, l => l.Stjuard1, z => z.ZaposleniId, (l, z) => z.Ime + " " + z.Prezime).FirstOrDefault().ToString();
            //    stj2 = db.Let.Where(l => l.LetId == letId)
            //                    .Join(db.Zaposleni, l => l.Stjuard2, z => z.ZaposleniId, (l, z) => z.Ime + " " + z.Prezime).FirstOrDefault().ToString();
            //}
            //else
            //{
                var stj1 = "Izaberi stjuarda";
                var stj2 = "Izaberi stjuarda";
            //}

            var jezik = db.Destinacija.Where(d => d.DestinacijaId == id).Select(d => d.Jezik).FirstOrDefault();
            List<int> stjuardiId = db.Stjuard.Where(s => s.JezikId == jezik).Select(s => s.StjuardId).ToList();
            var stjuardi = db.Zaposleni.Join(stjuardiId, z => z.ZaposleniId, s => s, (z, s) => z).ToList();

            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", stj1);
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", stj2);

            return PartialView();
        }

        [Authorize(Roles = "Admin, SuperUser")]
        // GET: Let/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Let let = await db.Let.FindAsync(id);

            if (let == null)
            {
                return HttpNotFound();
            }

            //List<Zaposleni> piloti = Zaposleni.ZaposleniPoRoli("Pilot");
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");

            List<Zaposleni> piloti = db.Pilot.Where(p => p.OcenaZS == true)
                              .Join(db.Zaposleni, p => p.SifraPilota, z => z.ZaposleniId, (p, z) => z).ToList();

            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Kopilot);
            ViewBag.Pilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Pilot);
            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard2);

            return View(let);
        }

        // POST: Let/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LetId,Destinacija,Avion,VremePoletanja,Pilot,Kopilot,Stjuard1,Stjuard2")] Let let)
        {
            if (ModelState.IsValid)
            {
                if (let.Stjuard1 == null || let.Stjuard2 == null || let.Pilot == null || let.Kopilot == null)
                {
                    ViewBag.Greska = "Let mora imati celokupnu posadu";
                }
                else if (let.Stjuard1 == let.Stjuard2)
                {
                    ViewBag.Greska = "Stjuardi moraju biti različiti";
                }
                else if (let.Pilot == let.Kopilot)
                {
                    ViewBag.Greska = "Pilot i kopilot moraju biti različiti";
                }
                else
                {
                    try
                    {
                        db.Entry(let).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {

                    }
                }
                
                
            }

            List<Zaposleni> piloti = db.Pilot.Where(p => p.OcenaZS == true)
                              .Join(db.Zaposleni, p => p.SifraPilota, z => z.ZaposleniId, (p, z) => z).ToList();
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");
            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona");
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv");
            ViewBag.Kopilot = new SelectList((from k in piloti select new { k.ZaposleniId, punoIme = k.Ime + " " + k.Prezime }), "ZaposleniId", "punoIme", let.Kopilot);
            ViewBag.Pilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Pilot);
            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard2);

            return View(let);
        }

        // GET: Let/Delete/5
        [Authorize(Roles = "Admin, SuperUser")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Let let = await db.Let.FindAsync(id);
            if (let == null)
            {
                return HttpNotFound();
            }
            return View(let);
        }

        // POST: Let/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Let let = await db.Let.FindAsync(id);
            db.Let.Remove(let);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Let/DetaljiLeta/5
        [Authorize(Roles = "Pilot, Stjuard")]
        public ActionResult DetaljiLeta(int? id)
        {
            // Vraća User Id trenutno ulogovanog usera
            var user = userManager.FindById(User.Identity.GetUserId());
            // Vraća rolu trenutno ulogovanog usera
            var role = roleManager.FindById(user.Roles.First().RoleId);
            // Vraća zaposlenog koji je trenutno ulogovan
            int zap = db.Zaposleni.Where(z => z.IdentityId == user.Id).Select(z => z.ZaposleniId).First();

            if (id == 0 || id == null)
            {
                var prviSledeci = db.Let
                                    .Where(l => l.Pilot == zap || l.Kopilot == zap || l.Stjuard1 == zap || l.Stjuard2 == zap)
                                    .OrderBy(l => l.VremePoletanja)
                                    .Select(l => l)
                                    .Where(l => l.VremePoletanja >= DateTime.Now)
                                    .FirstOrDefault();
                if (prviSledeci == null)
                {
                    return Content("Nema zakazanih letova");
                }
                else
                {
                    ViewBag.Destinacija = db.Destinacija.Where(d => d.DestinacijaId == prviSledeci.Destinacija).Select(d => d.Naziv).FirstOrDefault();
                    return PartialView(prviSledeci);
                }
            }
            else
            {
                var izabraniLet = db.Let
                                    .Where(l => l.Pilot == zap || l.Kopilot == zap || l.Stjuard1 == zap || l.Stjuard2 == zap)
                                    .Select(l => l).Where(l => l.LetId == id).First();
                ViewBag.Destinacija = db.Destinacija.Where(d => d.DestinacijaId == izabraniLet.Destinacija).Select(d => d.Naziv).FirstOrDefault();
                return PartialView(izabraniLet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
