using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Windshield.Models
{
    enum Gender {Male, Female};
    //Note that no validation occurs here.
    public class UserProfile
    {
        public Image avatar { get; set; }
        public String name { get; set; }
        public  uint age { get; set; }
        public  Gender gender { get; set; }
        
        //TODO: add numerations for countries.
        public String country { get; set; }
        public String occupation { get; set; }
        public string settings { get; set; }
    }
}