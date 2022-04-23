using System.ComponentModel.DataAnnotations;

namespace Parcial1.Models
{
    public class TicketViewModel
    {
        [Display(Name = "ID de la boleta")]
        [MaxLength(4, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Id { get; set; }
    }
}
