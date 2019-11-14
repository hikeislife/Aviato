﻿using Aviato.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Aviato.Models.ApplicationDbContext;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize(Roles = "SuperUser")]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                list.Add(new RoleViewModel(role));
            }
            return View(list);
        }

        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }
        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}