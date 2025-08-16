using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MantenimientoEstudiantes.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estudiante.")]
        [ForeignKey("Estudiante")]
        public int IdEstudiante { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripcion no puede superar los 200 caracteres")]
        public string Descripcion { get; set; }

        [Precision(5,2)]
        [Range(0, 100, ErrorMessage = "La calificacion debe estar entre 0 y 100")]
        public decimal Calificacion { get; set; }

        public Estudiante? Estudiante { get; set; }
    }
}
