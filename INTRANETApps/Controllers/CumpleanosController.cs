using INTRANETApps.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;



namespace INTRANETApps.Controllers
{
    public class CumpleanosController : Controller
    {

        public string GetConnectionString()
        {
            String connString = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracleserver)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=avacomp)));uid=intranet;pwd=intranet;";
            return connString;
        }

        public List<OracleEmpleadoModels> ConnectingToOracle(string birthdaysRange)
        {
            List<OracleEmpleadoModels> birthdaysList = new List<OracleEmpleadoModels>();

            string connectionString = GetConnectionString();
            using (OracleConnection connection = new OracleConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                string sql;
                if (birthdaysRange == "today")
                {
                    sql = "select * from avacomp.vw_datos_empleados where to_char(fecha_de_nacimiento,'MMDD') = to_char(sysdate,'MMDD')";
                    //sql = "select * from avacomp.vw_datos_empleados where nombre like '%MANUEL ALEJANDRO%'";
                    command.CommandText = sql;
                }

                if (birthdaysRange == "week")
                {
                    sql = "select * from avacomp.vw_datos_empleados where to_char(fecha_de_nacimiento,'MMDD') between to_char(sysdate + 1 ,'MMDD') AND to_char( sysdate + 5,'MMDD' ) ORDER BY to_char(fecha_de_nacimiento)";
                    command.CommandText = sql;
                }

                if (birthdaysRange == "all")
                {
                    sql = "select * from avacomp.vw_datos_empleados";
                    command.CommandText = sql;
                }

                OracleDataReader reader = command.ExecuteReader();
               
                
                while (reader.Read())
                {
                    OracleEmpleadoModels model = new OracleEmpleadoModels();

                    model.nombre = (string)reader["nombre"];
                    model.apellido = (string)reader["apellido"];
                    model.fecha_de_nacimiento = (DateTime)reader["fecha_de_nacimiento"];
                    model.salario_actual = (decimal)reader["salario_actual"];
                    model.cedula = (string)reader["cedula"];
                    model.carnet = (decimal)reader["carnet"];
                    model.id_posicion = (decimal)reader["id_posicion"];
                    model.nombre_posicion = (string)reader["nombre_posicion"];
                    model.nombre_departamento = (string)reader["nombre_departamento"];
                   // model.foto = (byte[])reader["foto"];
                    birthdaysList.Add(model);
                   
                }
            }
            return birthdaysList;
        }




        // GET: Cumpleanos
        public ActionResult Index() 
        {
            ViewBag.birthdaysList = ConnectingToOracle("today");
            return View();
        }

        public ActionResult WeekBirthdays()
        {
            ViewBag.birthdaysList = ConnectingToOracle("week");
            return PartialView();
        }

        public ActionResult FullBirthdayCalendar()
        {
            ViewBag.birthdaysList = ConnectingToOracle("all");
            return View();
        }


    }
}