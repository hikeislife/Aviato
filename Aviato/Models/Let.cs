namespace Aviato.Models
{
    using Aviato.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Let")]
    public partial class Let /*: IValidatableObject*/
    {
        public int LetId { get; set; }

        [Required(ErrorMessage ="Destinacija je obavezna")]
        [DestinacijaPoLetu("VremePoletanja", "LetId", ErrorMessage = "Na ovu destinaciju već imamo drugi let u isto vreme")]
        public int Destinacija { get; set; }

        [Required(ErrorMessage = "Avion je obavezan")]
        [AvionPoLetu("VremePoletanja", "LetId", ErrorMessage = "Ovaj avion je već zauzet na drugom letu")]
        public int Avion { get; set; }

        [Required(ErrorMessage = "Vreme poletanja je obavezno")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name =("Vreme poletanja"))]
        public DateTime VremePoletanja { get; set; }

        //[Required(ErrorMessage = "Pilot je obavezan")]
        [PosadaPoLetu("VremePoletanja", "LetId", ErrorMessage = "Ovaj pilot je već zauzet na drugom letu")]
        public int? Pilot { get; set; }

        //[Required(ErrorMessage = "Kopilot je obavezan")]
        [PosadaPoLetu("VremePoletanja", "LetId", ErrorMessage = "Ovaj kopilot je već zauzet na drugom letu")]
        public int? Kopilot { get; set; }

        //[Required(ErrorMessage = "Prvi stjuard je obavezan")]
        [PosadaPoLetu("VremePoletanja", "LetId", ErrorMessage = "Prvi stjuard je već zauzet na drugom letu")]
        public int? Stjuard1 { get; set; }

        //[Required(ErrorMessage = "Drugi stjuard je obavezan")]
        [PosadaPoLetu("VremePoletanja", "LetId", ErrorMessage = "Drugi stjuard je već zauzet na drugom letu")]
        public int? Stjuard2 { get; set; }

        public virtual Avion Avion1 { get; set; }

        public virtual Destinacija Destinacija1 { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }

        public virtual Zaposleni Zaposleni1 { get; set; }

        public virtual Zaposleni Zaposleni2 { get; set; }

        public virtual Zaposleni Zaposleni3 { get; set; }

        //id leta za kalendar
        public int IdLeta { get; set; }

        public int NepotpunaPosada { get; set; }

       public static List<Let> IstovremeniLetovi(DateTime vreme, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            // svi letovi koji su isto vreme
            List<Let> istovremeniLetovi = (from l in db.Let
                                           where l.VremePoletanja == vreme
                                           && l.LetId != id
                                           select l).ToList();
            return istovremeniLetovi;
        }
    }
}
