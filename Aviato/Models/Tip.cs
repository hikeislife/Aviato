namespace Aviato.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tip")]
    public partial class Tip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tip()
        {
            Avion = new HashSet<Avion>();
            Mehanicar = new HashSet<Mehanicar>();
        }

        public int TipId { get; set; }

        [StringLength(20)]
        [Display(Name ="Model")]
        public string NazivTipa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avion> Avion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mehanicar> Mehanicar { get; set; }
    }
}
