using Microsoft.AspNetCore.Mvc.Rendering;
using Parcial1.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parcial1.Models
{
    public class FormatTicketViewModel
    {
        public int Id { get; set; }

        public bool WasUsed { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        [Display(Name = "Entrada")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una entrada.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem>? Entrances { get; set; }

    }
}
