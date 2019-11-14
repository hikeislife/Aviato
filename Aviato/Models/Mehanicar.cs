namespace Aviato.Models
{
    using Aviato.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mehanicar")]
    public partial class Mehanicar
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MehanicarId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Licenca { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Datum sticanja")]
        [ProveraDatumaZaBuduceVreme(ErrorMessage = "Datum ne može biti u budućnosti")]
        public DateTime DatumLicence { get; set; }

        public virtual Tip Tip { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }
    }
}
