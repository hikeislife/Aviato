using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Aviato.Models.ApplicationDbContext;

namespace Aviato.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Molimo unesite naziv role")]
        [Display(Name ="Rola")]
        public string Name { get; set; }
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}