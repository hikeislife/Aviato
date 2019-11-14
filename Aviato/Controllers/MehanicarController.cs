using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;

namespace Aviato.Controllers
{   
    [HandleError]
    [Authorize(Roles = "Admin, SuperUser, Mehaničar")]
    public class MehanicarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mehanicar
        public async Task<ActionResult> Index()
        {
            var mehanicar = db.Mehanicar.Include(m => m.Tip).Include(m => m.Zaposleni);
            return View(await mehanicar.ToListAsync());
        }

        // GET: Mehanicar/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mehanicar mehanicar = await db.Mehanicar.FindAsync(id);
            if (mehanicar == null)
            {
                return HttpNotFound();
            }
            return View(mehanicar);
        }

        // GET: Mehanicar/Create
        public ActionResult Create()
        {
            ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa");
            ViewBag.MehanicarId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            return View();
        }

        // GET: Mehanicar/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mehanicar mehanicar = await db.Mehanicar.FindAsync(id);
            if (mehanicar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa", mehanicar.Licenca);
            ViewBag.MehanicarId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", mehanicar.MehanicarId);
            return View(mehanicar);
        }

        // POST: Mehanicar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MehanicarId,Licenca,DatumLicence")] Mehanicar mehanicar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mehanicar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa", mehanicar.Licenca);
            ViewBag.MehanicarId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", mehanicar.MehanicarId);
            return View(mehanicar);
        }

        // GET: Mehanicar/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mehanicar mehanicar = await db.Mehanicar.FindAsync(id);
            if (mehanicar == null)
            {
                return HttpNotFound();
            }
            return View(mehanicar);
        }

        // POST: Mehanicar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mehanicar mehanicar = await db.Mehanicar.FindAsync(id);
            db.Mehanicar.Remove(mehanicar);
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

        /////***********************************************/////
        ////Otvaranje Tipova u registraciji pri izboru role/////
        public ActionResult UcitavanjeTipova()
        {
            return PartialView("~/Views/Shared/MehanicarLicencaPW.cshtml");
        }
    }
}
