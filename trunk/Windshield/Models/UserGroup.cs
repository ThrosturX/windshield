using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
    public class UserGroup
    {
        public int id { get; set; }
        public int idOwner { get; set; }
        public String name { get; set; }
        //TODO: methods and validation.
    }
}