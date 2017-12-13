using MvcApp.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
    public class Physician
    {
        public ulong Id { get; set; }
       
        public string FirstName { get; set; }

        
        public string MiddleName { get; set; }

        
        public string LastName { get; set; }

        
        public DateTime BirthDate { get; set; }

       
        public string Gender { get; set; }

        
        public int? Weight { get; set; }

       
        public int? Height { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}