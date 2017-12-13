using PhysicianDirectory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhysicianDirectory.Controllers
{
    public class PhysicianController : Controller
    {
        // GET: Physician
        ServiceReference3.MyServiceClient ur = new ServiceReference3.MyServiceClient();
        static Physician physician = new Physician();
        static ContactInfo con = new ContactInfo();
        static Specialization spe = new Specialization();
        public ActionResult PersonalInformation(string searchstring)
        {
            TempData["search"] = searchstring;
            TempData.Keep();
            if (TempData["search"] != null)
            {

                var searchPhysician = from p in physicians
                                      where p.FirstName.ToLower().Contains(searchstring.ToLower()) || p.MiddleName.ToLower().Contains(searchstring.ToLower()) || p.LastName.ToLower().Contains(searchstring.ToLower()) 
                                      orderby p.Id
                                      select p;



                return View(searchPhysician);


            }
            else
            {
                List<Physician> lstRecord = new List<Physician>();

                var lst = ur.GetAllPersonalInfo();

                foreach (var item in lst)
                {
                    Physician usr = new Physician();
                    usr.Id = item.Id;
                    usr.FirstName = item.FirstName;
                    usr.MiddleName = item.MiddleName;
                    usr.LastName = item.LastName;
                    usr.BirthDate = item.BirthDate;
                    usr.Gender = item.Gender;
                    usr.Weight = item.Weight;
                    usr.Height = item.Height;
                    usr.ContactInfo = new ContactInfo
                    {
                        Id = item.Id,
                        HomeAddress = item.ContactInfo.HomeAddress,
                        HomePhone = item.ContactInfo.HomePhone,
                        OfficeAddress = item.ContactInfo.OfficeAddress,
                        OfficePhone = item.ContactInfo.OfficePhone,
                        EmailAdd = item.ContactInfo.EmailAdd,
                        CellphoneNumber = item.ContactInfo.CellphoneNumber,
                    };
                    usr.Specialization = new Specialization
                    {
                        Id = item.Id,
                        Name = item.Specialization.Name,
                        Description = item.Specialization.Description,

                    };
                    lstRecord.Add(usr);

                }
                TempData["physicianList"] = lstRecord;
                TempData.Keep();
                return View(lstRecord);



            }
        }
        public ActionResult ContactInformation()
        {
            if (TempData["search"] != null)
            {
                TempData.Keep();
                var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));


                return View(searchPhysician);


            }
            var physicianList = TempData["physicianList"];
            TempData.Keep();
            return View(physicianList);



        }

        public ActionResult SpecializationInformation()
        {
            if (TempData["search"] != null)
            {
                TempData.Keep();
                var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));


                return View(searchPhysician);


            }
            var physicianList = TempData["physicianList"];
            TempData.Keep();
            return View(physicianList);



        }

        public ActionResult AddUser()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(FormCollection collection, string next)
        {
            var lastId = ur.GetAllPersonalInfo().Any(phy => phy.Id >= 1);
            Physician usr = new Physician();

            try
            {
                if (next != null)
                {
                    if (ModelState.IsValid)
                    {
                        
                            if (lastId)
                            {
                                physician.Id = ur.GetAllPersonalInfo().Max(item => item.Id) + 1;
                            }

                            else
                            {
                                physician.Id = 1;
                            }

                            physician.FirstName = collection["FirstName"];
                            physician.MiddleName = collection["MiddleName"];
                            physician.LastName = collection["MiddleName"];
                            DateTime BirthDate;
                            DateTime.TryParse (collection["BirthDate"], out BirthDate);
                            physician.BirthDate = BirthDate;
                            physician.Gender = collection["Gender"];
                            string Height = collection["Height"];
                            physician.Height = Int32.Parse(Height);
                            string Weight = collection["Weight"];
                            physician.Weight = Int32.Parse(Weight);


                            return RedirectToAction("AddContact");
                        }
                    }
                
            }



            catch (Exception)
            {

            }
            return View();
        }
        public ActionResult AddContact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddContact(FormCollection collection, string next)
        {
            try
            {
                if (next != null)
                {
                    if (ModelState.IsValid)
                    {



                        con.HomeAddress = collection["HomeAddress"];
                        string HomePhone = collection["HomePhone"];
                        con.HomePhone = int.Parse(HomePhone);
                        con.OfficeAddress = collection["OfficeAddress"];
                        string OfficePhone = collection["OfficePhone"];
                        con.OfficePhone = int.Parse(OfficePhone);
                        con.EmailAdd = collection["EmailAdd"];
                        string CellphoneNumber = collection["CellphoneNumber"];
                        con.CellphoneNumber = long.Parse(CellphoneNumber);
                        return RedirectToAction("AddSpecialization");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Edit(int id)
        {

            Debug.WriteLine(id);
            var lst = ur.GetAllUserById(id);
            Physician usr = new Physician();
           
            //usr.Id = lst.Id;
            usr.FirstName = lst.FirstName;
            usr.MiddleName = lst.MiddleName;
            usr.LastName = lst.LastName;
            usr.BirthDate = lst.BirthDate;
            usr.Gender = lst.Gender;
            usr.Height = lst.Height;
            usr.Weight = lst.Weight;

            usr.ContactInfo = new ContactInfo
            {
                HomeAddress = lst.ContactInfo.HomeAddress,
                HomePhone = lst.ContactInfo.HomePhone,
                OfficeAddress = lst.ContactInfo.OfficeAddress,
                OfficePhone = lst.ContactInfo.OfficePhone,
                EmailAdd = lst.ContactInfo.EmailAdd,
                CellphoneNumber = lst.ContactInfo.CellphoneNumber
            };
            usr.Specialization = new Specialization
            {
                Name = lst.Specialization.Name,
                Description = lst.Specialization.Description
            };
            return View(usr);

        }
        [HttpPost]
        public ActionResult Edit(Physician phy)
        {
            Physician usr = new Physician();
            usr.Id = phy.Id;
            usr.FirstName = phy.FirstName;
            usr.MiddleName = phy.MiddleName;
            usr.LastName = phy.LastName;
            usr.BirthDate = phy.BirthDate;
            usr.Gender = phy.Gender;
            usr.Height = phy.Height;
            usr.Weight = phy.Weight;

            usr.ContactInfo = new ContactInfo
            {
                
                HomeAddress = phy.ContactInfo.HomeAddress,
                HomePhone = phy.ContactInfo.HomePhone,
                OfficeAddress = phy.ContactInfo.OfficeAddress,
                OfficePhone = phy.ContactInfo.OfficePhone,
                EmailAdd = phy.ContactInfo.EmailAdd,
                CellphoneNumber = phy.ContactInfo.CellphoneNumber
            };
            usr.Specialization = new Specialization
            {
                
                Name = phy.Specialization.Name,
                Description = phy.Specialization.Description
            };
           

            if (TryUpdateModel(phy))
            {


                int Retval = ur.UpdateUser(usr.Id, usr.FirstName, usr.MiddleName, usr.LastName, 
                    usr.BirthDate, usr.Gender, usr.Weight, usr.Height, usr.ContactInfo.HomeAddress, 
                    usr.ContactInfo.HomePhone, usr.ContactInfo.OfficeAddress, usr.ContactInfo.OfficePhone, 
                    usr.ContactInfo.EmailAdd, usr.ContactInfo.CellphoneNumber, usr.Specialization.Name, 
                    usr.Specialization.Description);
                if (Retval > 0)
                {
                    return RedirectToAction("PersonalInformation");
                }
            }
            return View();

        }
        public ActionResult AddSpecialization()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddSpecialization(string next, FormCollection collection)
        {

            try
            {
                spe.Name = collection["Name"];
                spe.Description = collection["Description"];
                Physician usr = new Physician();


                usr.Id = physician.Id;
                usr.FirstName = physician.FirstName;
                usr.MiddleName = physician.MiddleName;
                usr.LastName = physician.LastName;
                usr.BirthDate = physician.BirthDate;
                usr.Gender = physician.Gender;
                usr.Height = physician.Height;
                usr.Weight = physician.Weight;

                usr.ContactInfo = new ContactInfo
                {
                    Id = physician.Id,
                    HomeAddress = con.HomeAddress,
                    HomePhone = con.HomePhone,
                    OfficeAddress = con.OfficeAddress,
                    OfficePhone = con.OfficePhone,
                    EmailAdd = con.EmailAdd,
                    CellphoneNumber = con.CellphoneNumber
                };
                usr.Specialization = new Specialization
                {
                    Id = physician.Id,
                    Name = spe.Name,
                    Description = spe.Description
                };

                ur.AddUser(usr.Id, usr.FirstName, usr.MiddleName, usr.LastName, usr.BirthDate, usr.Gender, 
                    usr.Weight, usr.Height, usr.ContactInfo.HomeAddress, usr.ContactInfo.HomePhone, 
                    usr.ContactInfo.OfficeAddress, usr.ContactInfo.OfficePhone, usr.ContactInfo.EmailAdd, 
                    usr.ContactInfo.CellphoneNumber, usr.Specialization.Name, usr.Specialization.Description);
                physicians.Add(usr);
                //ContactInfo cont = new ContactInfo();
                //Specialization spec = new Specialization();
                return RedirectToAction("PersonalInformation");
            }
            
            catch (Exception )
            {
                
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            Debug.WriteLine(id);
            var lst = ur.GetAllUserById(id);
            Physician usr = new Physician();

            //usr.Id = lst.Id;
            usr.FirstName = lst.FirstName;
            usr.MiddleName = lst.MiddleName;
            usr.LastName = lst.LastName;
            usr.BirthDate = lst.BirthDate;
            usr.Gender = lst.Gender;
            usr.Height = lst.Height;
            usr.Weight = lst.Weight;

            usr.ContactInfo = new ContactInfo
            {
                HomeAddress = lst.ContactInfo.HomeAddress,
                HomePhone = lst.ContactInfo.HomePhone,
                OfficeAddress = lst.ContactInfo.OfficeAddress,
                OfficePhone = lst.ContactInfo.OfficePhone,
                EmailAdd = lst.ContactInfo.EmailAdd,
                CellphoneNumber = lst.ContactInfo.CellphoneNumber
            };
            usr.Specialization = new Specialization
            {
                Name = lst.Specialization.Name,
                Description = lst.Specialization.Description
            };
            return View(usr);
        }

        
        [HttpPost]
        public ActionResult Delete(int id, Physician phy)
        {
           
            int retval = ur.DeleteUserById(id);
            if (retval > 0)
            {
                return RedirectToAction("PersonalInformation");
            }

            return View();
        }
        static List<Physician> physicians = new List<Physician>();
    }
    
}


    





