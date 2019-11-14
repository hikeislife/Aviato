using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;
using Aviato.ViewModels;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize(Roles = "Admin, SuperUser")]
    public class ZaposleniController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Zaposleni
        public async Task<ActionResult> Index()
        {
            var prikaziZaposlene = (from u in db.Users join z in db.Zaposleni on u.Id equals z.IdentityId
                                   select new
                                  {
                                      z.ZaposleniId,
                                      z.Ime,
                                      z.Prezime,
                                      u.Email,
                                      Jmbg = z.JMBG,
                                      z.GodinaRodjenja,
                                      Pozicija = (from ur in u.Roles
                                                   join r in db.Roles on ur.RoleId
                                                   equals r.Id
                                                   select r.Name).ToList()
                                  }).ToList().Select(p => new ZaposleniViewModel()
                                  {
                                      ZaposleniId = p.ZaposleniId,
                                      ImeIPrezime = p.Ime + ' ' + p.Prezime,
                                      Email = p.Email,
                                      JMBG = p.Jmbg,
                                      GodinaRodjenja = p.GodinaRodjenja,
                                      Pozicija = string.Join(",", p.Pozicija)
                                  }).OrderBy(p => p.Pozicija);

            ViewBag.ZapPoRoli = new SelectList(db.Roles, "Id", "Name");

            return View(prikaziZaposlene);
        }

        public ActionResult IndexPoRoli(string rola)
        {
            //ZaposleniViewModel zvm = new ZaposleniViewModel();

            List<ZaposleniViewModel> zvm = new List<ZaposleniViewModel>();

            var zapPoRoli = Zaposleni.ZaposleniPoRoli(rola);
            
            foreach (var it in zapPoRoli)
            {
                ZaposleniViewModel z = new ZaposleniViewModel();
                var email = db.Users.Where(u => u.Id == it.IdentityId).Select(u => u.Email).FirstOrDefault();
                z.ZaposleniId = it.ZaposleniId;
                z.ImeIPrezime = it.Ime + ' ' + it.Prezime;
                z.Email = email;
                z.Pozicija = rola;

                zvm.Add(z);
            }

            ViewBag.ZapPoRoli = new SelectList(db.Roles, "Id", "Name");

            return PartialView(zvm);
        }



        // GET: Zaposleni/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);

            var email = db.Users.Where(u => u.Id == zaposleni.IdentityId).Select(u => u.Email).FirstOrDefault();
            ViewBag.email = email;

            var rola = string.Join(", ", getRoleByUserId(zaposleni.IdentityId));
            ViewBag.Pozicija = rola;

            EditZaposleniViewModel ezvm = new EditZaposleniViewModel()
            {
                Zaposleni = zaposleni
            };

            if (ViewBag.Pozicija == "Pilot")
            {
                Pilot pilotInfo = (from p in db.Pilot where p.SifraPilota == id select p).FirstOrDefault();
                ezvm.Pilot = pilotInfo;
            }
            else if (ViewBag.Pozicija == "Stjuard")
            {
                ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici");
                ICollection<Stjuard> stjuardInfo = (from s in db.Stjuard where s.StjuardId == id select s).ToList();
                ezvm.Stjuard = stjuardInfo;
            }
            else if (ViewBag.Pozicija == "Mehaničar")
            {
                ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa");
                ICollection<Mehanicar> mehanicariInfo = (from m in db.Mehanicar where m.MehanicarId == id select m).ToList();
                ezvm.Mehanicar = mehanicariInfo;
            }

            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(ezvm);
        }

        // GET: Zaposleni/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);

            var rola = getRoleByUserId(zaposleni.IdentityId);

            ViewBag.Pozicija = string.Join(", ", rola);
            EditZaposleniViewModel EZVM = new EditZaposleniViewModel()
            {
                Zaposleni = zaposleni
            };

            EditZaposleniPomocna(EZVM, ViewBag.Pozicija);

            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(EZVM);
        }

        // POST: Zaposleni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ZaposleniId,JMBG,Ime,Prezime,GodinaRodjenja,IdentityId")] Zaposleni zaposleni,
                                             [Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot,
                                              EditZaposleniViewModel EZVM, string pozicija)
        {
            if (ModelState.IsValid)
            {   

                zaposleni.ZaposleniId = EZVM.Zaposleni.ZaposleniId;
                zaposleni.IdentityId = EZVM.Zaposleni.IdentityId;
                zaposleni.Ime = EZVM.Zaposleni.Ime;
                zaposleni.Prezime = EZVM.Zaposleni.Prezime;
                zaposleni.JMBG = EZVM.Zaposleni.JMBG;
                zaposleni.GodinaRodjenja = EZVM.Zaposleni.GodinaRodjenja;

                if (EZVM.Pilot != null)
                {
                    pilot.PilotId = EZVM.Pilot.PilotId;
                    pilot.PoslednjiMedicinski = EZVM.Pilot.PoslednjiMedicinski;
                    pilot.OcenaZS = EZVM.Pilot.OcenaZS;
                    pilot.SatiLetenja = EZVM.Pilot.SatiLetenja;
                    pilot.SifraPilota = EZVM.Zaposleni.ZaposleniId;

                    if (ModelState.IsValid)
                    {   
                        db.Entry(pilot).State = EntityState.Modified;
                    }
                }

                if (EZVM.promenaJezika != null)
                {
                    db.Stjuard.RemoveRange(db.Stjuard.Where(x => x.StjuardId == EZVM.Zaposleni.ZaposleniId));
                    await db.SaveChangesAsync();

                    //try
                    //{
                        var jezici = EZVM.promenaJezika.Split(',');
                        foreach (var j in jezici)
                        {
                            Stjuard stju = new Stjuard();
                            stju.StjuardId = EZVM.Zaposleni.ZaposleniId;
                            stju.JezikId = Convert.ToInt32(j);

                            db.Stjuard.Add(stju);
                        }
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.Write(e);
                    //}
                    await db.SaveChangesAsync();
                }

                else if (EZVM.promenaLicenci != null)
                {
                    db.Mehanicar.RemoveRange(db.Mehanicar.Where(x => x.MehanicarId == EZVM.Zaposleni.ZaposleniId));
                    await db.SaveChangesAsync();

                    var licence = EZVM.promenaLicenci.Split(',');
                    var datumi = EZVM.promenaDatuma.Split(',');
                    for (var l = 0; l < licence.Length; l++)
                    {
                        Mehanicar meh = new Mehanicar();
                        meh.MehanicarId = EZVM.Zaposleni.ZaposleniId;
                        meh.Licenca = Convert.ToInt32(licence[l]);
                        meh.DatumLicence = Convert.ToDateTime(datumi[l]);

                        db.Mehanicar.Add(meh);
                    }
                    await db.SaveChangesAsync();
                }

                db.Entry(zaposleni).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Pozicija = pozicija;

                EditZaposleniPomocna(EZVM, pozicija);
                
                return View(EZVM);
            }

            return View(EZVM);
        }

        // GET: Zaposleni/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }

            return View(zaposleni);
        }

        // POST: Zaposleni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);

            var userId = zaposleni.IdentityId;
            var user = db.Users.Find(userId);

            var letovi = db.Let.Where(l => l.Pilot == zaposleni.ZaposleniId || l.Kopilot == zaposleni.ZaposleniId
                                     || l.Stjuard1 == zaposleni.ZaposleniId || l.Stjuard2 == zaposleni.ZaposleniId)
                                     .Select(l => l).ToList();

            foreach (var let in letovi)
            {
                if (let.Pilot == zaposleni.ZaposleniId)
                {
                    let.Pilot = null;
                    let.NepotpunaPosada = 1;
                }
                else if (let.Kopilot == zaposleni.ZaposleniId)
                {
                    let.Kopilot = null;
                    let.NepotpunaPosada = 1;
                }
                else if (let.Stjuard1 == zaposleni.ZaposleniId)
                {
                    let.Stjuard1 = null;
                    let.NepotpunaPosada = 1;
                }
                else 
                {
                    let.Stjuard2 = null;
                    let.NepotpunaPosada = 1;
                }
            }
            
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //VRATI ROLU
        private IEnumerable<string> getRoleByUserId(string id)
        {
            var rola = (from U in db.Users
                        where U.Id == id
                        select new
                        {
                            Pozicija = (from UR in U.Roles
                                        join R in db.Roles
                                        on UR.RoleId equals R.Id
                                        select R.Name)
                        }).First();
            return rola.Pozicija;
        }

        private void EditZaposleniPomocna(EditZaposleniViewModel EZVM, string pozicija)
        {
            if (ViewBag.Pozicija == "Pilot")
            {
                Pilot pilotInfo = (from p in db.Pilot
                                   where p.SifraPilota == EZVM.Zaposleni.ZaposleniId
                                   select p).First();
                EZVM.Pilot = pilotInfo;
            }

            if (ViewBag.Pozicija == "Mehaničar")
            {
                ICollection<Mehanicar> mehanicariInfo = (from m in db.Mehanicar
                                                         where m.MehanicarId == EZVM.Zaposleni.ZaposleniId
                                                         select m).ToList();
                ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa");
                EZVM.Mehanicar = mehanicariInfo;
            }
            if (ViewBag.Pozicija == "Stjuard")
            {
                ICollection<Stjuard> stjuardiInfo = (from s in db.Stjuard
                                                     where s.StjuardId == EZVM.Zaposleni.ZaposleniId
                                                     select s).ToList();
                ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici");
                EZVM.Stjuard = stjuardiInfo;
            }
        }

    }
}
