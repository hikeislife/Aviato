using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Aviato.Models;

namespace Aviato.ViewModels
{
    public class EditZaposleniViewModel
    {
        public EditZaposleniViewModel()
        {
            Mehanicar = new HashSet<Mehanicar>();
            Stjuard = new HashSet<Stjuard>();
            Let = new HashSet<Let>();
            Avion = new HashSet<Avion>();
        }

        [Key]
        public int Id { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Pilot Pilot { get; set; }
        public ICollection<Mehanicar> Mehanicar { get; set; }
        public ICollection<Stjuard> Stjuard { get; set; }
        public ICollection<Let> Let { get; set; }
        public ICollection<Avion> Avion { get; set; }
        public RoleViewModel RoleViewModel { get; set; }
        public string promenaLicenci { get; set; }
        public string promenaDatuma { get; set; }
        public string promenaJezika { get; set; }
    }
}