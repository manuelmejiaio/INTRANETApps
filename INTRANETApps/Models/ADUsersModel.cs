using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INTRANETApps.Models
{
    public class ADUsersModel
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string extension { get; set; }
        public string posicion { get; set; }
    }
}