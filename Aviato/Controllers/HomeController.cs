using Aviato.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using Aviato.ViewModels;
using System.Threading.Tasks;
using System.Net;
using System.Web.Security;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

        public async Task<ActionResult> Index()
        {
            //update sati letenja pilota pri logovanju
            Pilot.SatiLetenjaPilota();

            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var role = await roleManager.FindByIdAsync(user.Roles.FirstOrDefault().RoleId);

            ViewBag.Id = user.Id;
            ViewBag.Email = user.Email;
            ViewBag.Rola = role.Name;

            EditZaposleniViewModel ezvm = new EditZaposleniViewModel();

            ezvm.Zaposleni = db.Zaposleni.Where(z => z.IdentityId == user.Id).Select(z => z).FirstOrDefault();
            int id = ezvm.Zaposleni.ZaposleniId;

            if (role.Name == "Pilot")
            {
                ezvm.Pilot = db.Pilot.Where(p => p.SifraPilota == id).Select(p => p).FirstOrDefault();
                ezvm.Let = db.Let.Where(l => l.Pilot == id || l.Kopilot == id).Select(l => l).Where(l => l.VremePoletanja > DateTime.Now).OrderBy(l => l.VremePoletanja).ToList();

            }
            else if (role.Name == "Stjuard")
            {
                ezvm.Let = db.Let.Where(l => l.Stjuard1 == id || l.Stjuard2 == id).Select(l => l).Where(l => l.VremePoletanja > DateTime.Now).OrderBy(l => l.VremePoletanja).ToList();
            }
            else if (role.Name == "Mehaničar")
            {
                ezvm.Mehanicar = db.Mehanicar.Where(m => m.MehanicarId == id).Select(m => m).ToList();
                ezvm.Avion = (from a in db.Avion
                              join m in db.Mehanicar on a.TipAviona equals m.Tip.TipId
                              where a.ServisniStatus == true && m.MehanicarId == id
                              select a).ToList();
            }

            return View(ezvm);
        }

        // GET: Avion/Edit/5
        [Authorize(Roles = "Mehaničar, Admin")]
        public async Task<ActionResult> ServisAviona(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Avion avion = await db.Avion.FindAsync(id);

            if (avion == null)
            {
                return HttpNotFound();
            }

            return View(avion);
        }

        // POST: Avion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ServisAviona(Avion avion)
        {
            Avion servis = db.Avion.FirstOrDefault(a => a.AvionId == avion.AvionId);

            if (servis == null)
            {
                return HttpNotFound();
            }

            servis.ServisniStatus = avion.ServisniStatus;

            if (ModelState.IsValid)
            {   
                await db.SaveChangesAsync();

                if (!Roles.IsUserInRole("Mehaničar"))
                {
                    return RedirectToAction("Index", "Avion");
                }
                else if (Roles.IsUserInRole("Mehaničar"))
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(avion);
        }

        public JsonResult SkupiLetove()
        {
            int id = TrenutnoUlogovaniZaposleni();
            db.Configuration.ProxyCreationEnabled = false;
            var letovi = db.Let.Where(l => l.Pilot == id || l.Kopilot == id || l.Stjuard1 == id || l.Stjuard2 == id).Select(l => l).ToList();
            return new JsonResult { Data = letovi, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public int TrenutnoUlogovaniZaposleni()
        {
            // Vraća User Id trenutno ulogovanog usera
            var user = userManager.FindById(User.Identity.GetUserId());
            // Vraća rolu trenutno ulogovanog usera
            var role = roleManager.FindById(user.Roles.First().RoleId);
            // Vraća zaposlenog koji je trenutno ulogovan
            int id = db.Zaposleni.Where(z => z.IdentityId == user.Id).Select(z => z.ZaposleniId).First();
            return id;
        }
    }
}