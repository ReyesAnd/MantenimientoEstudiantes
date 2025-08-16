using MantenimientoEstudiantes.Models;
using System.Collections.Generic;

namespace MantenimientoEstudiantes.Models
{
    public class DashboardViewModel
    {
        public int TotalEstudiantes { get; set; }
        public int TotalTareas { get; set; }
        public decimal PromedioCalificaciones { get; set; }
        public List<Estudiante> UltimosEstudiantes { get; set; }
    }
}