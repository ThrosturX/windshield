using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{

    public class User
    {
        public int id { get; set; }
        public String password { get; set; }

        public UserProfile profile = null;
        //TODO: methods and validation.
    }
}