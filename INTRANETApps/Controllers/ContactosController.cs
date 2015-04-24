using INTRANETApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;


namespace INTRANETApps.Controllers
{
    public class ContactosController : Controller
    {
        // GET: Contactos
        public ActionResult Index()
        {
            List<ADUsersModel> listADUsers = new List<ADUsersModel>();
            using (var context = new PrincipalContext(ContextType.Domain, "map-dc-02.presidencia.local"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    
                    foreach (var result in searcher.FindAll())
                    {
                        ADUsersModel ADModel = new ADUsersModel();
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;



                        int flags = (int)de.Properties["userAccountControl"].Value;
                        if (!Convert.ToBoolean(flags & 0x00000002) && (string)de.Properties["givenName"].Value != null && (string)de.Properties["mail"].Value != null && (string)de.Properties["sn"].Value != null)
                        {
                            ADModel.userName = (string)de.Properties["givenName"].Value;
                            ADModel.userLastName = (string)de.Properties["sn"].Value;
                            ADModel.extension = (string)de.Properties["physicalDeliveryOfficeName"].Value;
                            ADModel.posicion = (string)de.Properties["title"].Value;
                            ADModel.email = (string)de.Properties["mail"].Value;

                            listADUsers.Add(ADModel);
                        }
                    }
                }
            }

            return View(listADUsers.OrderBy(a => a.userName));
        }
    }
}