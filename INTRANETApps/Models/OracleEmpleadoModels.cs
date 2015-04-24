using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace INTRANETApps.Models
{
    public class OracleEmpleadoModels
    {
        public string nombre { get; set; }

        public string apellido { get; set; }
        public DateTime fecha_de_nacimiento { get; set; }
        public decimal salario_actual { get; set; }
        public  string cedula { get; set; }
        public Decimal carnet { get; set; }
        public Decimal id_posicion { get; set; }
        public string nombre_posicion { get; set; }
        public string nombre_departamento { get; set; }
       // public byte[] foto { get; set; }
    }
}