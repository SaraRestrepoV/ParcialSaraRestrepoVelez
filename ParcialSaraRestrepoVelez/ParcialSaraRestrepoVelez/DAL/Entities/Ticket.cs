using System.ComponentModel.DataAnnotations;

namespace ParcialSaraRestrepoVelez.DAL.Entities
{
    public class Ticket : Entity
    {
        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }
        [Display(Name = "Fue usada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Boolean? IsUsed { get; set; }
        [Display(Name = "Portería entrada")]
        public string? EntranceGate { get; set; }
    }
}
