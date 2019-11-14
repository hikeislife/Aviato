namespace Aviato.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Jezik")]
    public partial class Jezik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jezik()
        {
            Destinacija = new HashSet<Destinacija>();
            Stjuard = new HashSet<Stjuard>();
        }

        public int JezikId { get; set; }

        [Column("Jezik")]
        [StringLength(20)]
        [Display(Name = "Jezik")]
        public string Jezici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Destinacija> Destinacija { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stjuard> Stjuard { get; set; }
    }
}
