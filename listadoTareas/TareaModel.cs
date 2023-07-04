using System;

namespace listadoTareas
{
    public class TareaModel
    {
        [ColumnSqlAttribute("Id")]
        public int Id { get; set; }
        [ColumnSqlAttribute("Asignatura")]
        public string Asignatura { get; set; }
        [ColumnSqlAttribute("Mandato")]
        public string Mandato { get; set; }
        [ColumnSqlAttribute("FechaAsignacion")]
        public DateTime FechaAsignacion { get; set; }
        [ColumnSqlAttribute("FechaEntrega")]
        public DateTime FechaEntrega { get; set; }
        [ColumnSqlAttribute("Descripcion")]
        public string Descripcion { get; set; }
        [ColumnSqlAttribute("Activo")]
        public string Activo { get; set; }
    }
}