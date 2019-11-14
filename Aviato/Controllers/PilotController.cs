using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;

namespace Aviato.Controllers
{   
    [HandleError]
    [Authorize(Roles = "Admin, SuperUser, Pilot")]
    public class PilotController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pilot
        public async Task<ActionResult> Index()
        {
            var pilot = db.Pilot.Include(p => p.Zaposleni);
            return View(await pilot.ToListAsync());
        }

        // GET: Pilot/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // GET: Pilot/Create
        public ActionResult Create()
        {
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            return PartialView();
        }

        // GET: Pilot/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", pilot.SifraPilota);
            return View(pilot);
        }

        // POST: Pilot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pilot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", pilot.SifraPilota);
            return View(pilot);
        }

        // GET: Pilot/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // POST: Pilot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pilot pilot = await db.Pilot.FindAsync(id);
            db.Pilot.Remove(pilot);
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

        /////******************************************************/////
        /////Otvaranje Zdravstvenog u registraciji pri izboru role/////
        public ActionResult ZdravstvenoStanje()
        {
            return PartialView("~/Views/Shared/PilotZdravstvenoPW.cshtml");
        }
    }
}
