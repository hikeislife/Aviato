namespace Aviato.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stjuard")]
    public partial class Stjuard
    {   
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JezikId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StjuardId { get; set; }

        public virtual Jezik Jezik { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }
    }
}
