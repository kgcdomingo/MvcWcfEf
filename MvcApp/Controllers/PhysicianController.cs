using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class PhysicianController : Controller
    {
        // GET: Physician
        ServiceReference1.MyServiceClient ur = new ServiceReference1.MyServiceClient();
        public ActionResult PersonalInformation()
        {
            List<Physician> lstRecord = new List<Physician>();

            var lst = ur.GetAllPersonalInfo();

            foreach (var item in lst)
            {
                Physician usr = new Physician();

                usr.FirstName = item.FirstName;
                usr.MiddleName = item.MiddleName;
                usr.LastName = item.LastName;
                usr.BirthDate = item.BirthDate;
                usr.Gender = item.Gender;
                usr.Weight = item.Weight;
                usr.Height = item.Height;
                lstRecord.Add(usr);

            }


            return View(lstRecord);
        }

    }
}