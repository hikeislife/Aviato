using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Aviato.ViewModels
{
    public class ZaposleniViewModel
    {   
        [Key]
        public int Id { get; set; } 

        [Display(Name = "Ime i prezime")]
        public string ImeIPrezime { get; set; }

        public int GodinaRodjenja { get; set; }

        public string JMBG { get; set; }

        public string Email { get; set; }

        public string Pozicija { get; set; }

        public int ZaposleniId { get; set; }
    }
}