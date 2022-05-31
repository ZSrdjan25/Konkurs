using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Kandidat
    {
        [Key]
        public int KandidatId { get; set; }
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }
        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }
        [Required]
        [StringLength(13)]
        public string JMBG { get; set; }
        [Required]
        [Display(Name ="Godina rodjenja")]
        public DateTime GodinaRodjenja { get; set; }
        [StringLength(33)]
        public string Telefon { get; set; }
        [StringLength(100)]
        public string Napomena { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Kandidat zaposlen")]
        public string KandidatZaposlen { get; set; }
        [Required]
        [Display(Name = "Datum izmjene podataka")]
        public DateTime DatumIzmjenePodataka { get; set; }
    }
}
