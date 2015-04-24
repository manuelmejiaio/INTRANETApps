using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OracleClient;
using INTRANETApps.Models;

namespace INTRANETApps.Controllers
{
    public class MenuController : Controller
    {

        public string GetConnectionString()
        {
            String connString = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracleserver)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=avacomp)));uid=intranet;pwd=intranet;";
            return connString;
        }

        public List<OracleMenuModels> ConnectingToOracle()
        {
            List<OracleMenuModels> menuList = new List<OracleMenuModels>();

            string connectionString = GetConnectionString();
            using (OracleConnection connection = new OracleConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                //select * from avacomp.almuerzo_guardia_pres 
                string sql;
                sql = "select * from avacomp.almuerzo_guardia_pres where to_char(fecha_publicacion,'MMDDYY') = to_char(sysdate,'MMDDYY')";
                command.CommandText = sql;
                

                OracleDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    OracleMenuModels model = new OracleMenuModels();
                    model.descripcion = (string)reader["descripcion"];
                    model.fechaDePublicacion = (DateTime)reader["fecha_publicacion"];
                    model.contenido = (string)reader["contenido"];
                    menuList.Add(model);
                }
            }
            return menuList;
        }



        // GET: Menu
        //select * from avacomp.almuerzo_guardia_pres
        public ActionResult Index()
        {
            ViewBag.menuList = ConnectingToOracle();
            return View();
        }
    }
}