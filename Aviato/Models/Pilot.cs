namespace Aviato.Models
{
    using Aviato.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("Pilot")]
    public partial class Pilot
    {
        public int PilotId { get; set; }

        [Required(ErrorMessage ="Poslednji lekarski pregled je obavezan")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Poslednji lekarski pregled")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ProveraDatumaZaBuduceVreme(ErrorMessage ="Datum ne može biti u budućnosti")]
        public DateTime PoslednjiMedicinski { get; set; }

        [Display(Name = "Zdravstveno stanje")]
        public bool OcenaZS { get; set; }

        [Required(ErrorMessage = "Sati letenja su obavezni")]
        [Display(Name = "Sati letenja")]
        public int SatiLetenja { get; set; }

        public int SifraPilota { get; set; }

        public DateTime? PoslednjeAzuriranje { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }

        //racunanje sata letenja pilota
        public static async void SatiLetenjaPilota()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var piloti = db.Pilot.Select(p => p).ToList();
            var datum = DateTime.Now.Date;

            
            foreach (var pil in piloti)
            {
                if (pil.PoslednjeAzuriranje == null)
                {
                    int zbirSatiProslihLetova = db.Let.Where(l => l.Pilot == pil.SifraPilota || l.Kopilot == pil.SifraPilota)
                                                      .Where(l => l.VremePoletanja <= DateTime.Now)
                                                      .Join(db.Destinacija, l => l.Destinacija, d => d.DestinacijaId, (l, d) => d.TrajanjeLeta).ToList().Sum();

                    pil.SatiLetenja = zbirSatiProslihLetova + pil.SatiLetenja;
                    pil.PoslednjeAzuriranje = datum;
                    db.Entry(pil).State = EntityState.Modified;
                    await db.SaveChangesAsync(); 
                }
                else if (pil.PoslednjeAzuriranje < datum)
                {
                    int zbirSatiLeta = pil.SatiLetenja;
                    int zbirSatiProslihLetova = db.Let.Where(l => l.Pilot == pil.SifraPilota || l.Kopilot == pil.SifraPilota)
                                                      .Where(l => l.VremePoletanja > pil.PoslednjeAzuriranje && l.VremePoletanja <= datum)
                                                      .Join(db.Destinacija, l => l.Destinacija, d => d.DestinacijaId, (l, d) => d.TrajanjeLeta).ToList().Sum();

                    pil.SatiLetenja = zbirSatiProslihLetova + zbirSatiLeta;
                    pil.PoslednjeAzuriranje = datum;
                    db.Entry(pil).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}

