using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aviato.Models;
using Aviato.ViewModels;

namespace Aviato.Controllers
{
    public class EditZaposleniViewModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EditZaposleniViewModel
        public async Task<ActionResult> Index()
        {
            return View(await db.EditZaposleniViewModels.ToListAsync());
        }

        // GET: EditZaposleniViewModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditZaposleniViewModel editZaposleniViewModel = await db.EditZaposleniViewModels.FindAsync(id);
            if (editZaposleniViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editZaposleniViewModel);
        }

        // GET: EditZaposleniViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditZaposleniViewModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] EditZaposleniViewModel editZaposleniViewModel)
        {
            if (ModelState.IsValid)
            {
                db.EditZaposleniViewModels.Add(editZaposleniViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(editZaposleniViewModel);
        }

        // GET: EditZaposleniViewModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditZaposleniViewModel editZaposleniViewModel = await db.EditZaposleniViewModels.FindAsync(id);
            if (editZaposleniViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editZaposleniViewModel);
        }

        // POST: EditZaposleniViewModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] EditZaposleniViewModel editZaposleniViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editZaposleniViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editZaposleniViewModel);
        }

        // GET: EditZaposleniViewModel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditZaposleniViewModel editZaposleniViewModel = await db.EditZaposleniViewModels.FindAsync(id);
            if (editZaposleniViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editZaposleniViewModel);
        }

        // POST: EditZaposleniViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EditZaposleniViewModel editZaposleniViewModel = await db.EditZaposleniViewModels.FindAsync(id);
            db.EditZaposleniViewModels.Remove(editZaposleniViewModel);
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
    }
}
