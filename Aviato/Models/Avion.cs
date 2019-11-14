namespace Aviato.Models
{
    using Aviato.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Avion")]
    public partial class Avion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avion()
        {
            Let = new HashSet<Let>();
        }

        public int AvionId { get; set; }

        [Display(Name ="Avion")]
        public string SifraAviona { get; set; }

        [Display(Name = "Godina proizvodnje")]
        //[RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Godina nije validna")]
        [GodinaProizvodnjeAvion(ErrorMessage ="Godina ne moze biti u budućnosti")]
        public int GodinaProizvodnje { get; set; }

        [Display(Name = "Servisni status")]
        public bool ServisniStatus { get; set; }

        [Display(Name = "Model")]
        public int TipAviona { get; set; }

        public virtual Tip Tip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let { get; set; }
    }
}
