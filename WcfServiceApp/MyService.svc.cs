using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    public class MyService : IMyService
    {
        public void DoWork()
        {
        }
        public List<Physician> GetAllPersonalInfo()
        {
            List<Physician> userlst = new List<Physician>();
            TestDBEntities tstDb = new TestDBEntities();
            var lstUsr = from k in tstDb.Physicians select k;
            foreach (var item in lstUsr)
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

                userlst.Add(usr);

            }

            return userlst;
        }
        public List<Physician> GetAllContactInfo()
        {
            List<Physician> userlst = new List<Physician>();
            TestDBEntities tstDb = new TestDBEntities();
            var lstUsr = from k in tstDb.Physicians select k;
            foreach (var item in lstUsr)
            {
                Physician usr = new Physician();

                usr.ContactInfo.HomeAddress = item.ContactInfo.HomeAddress;
                usr.ContactInfo.HomePhone = item.ContactInfo.HomePhone;
                usr.ContactInfo.OfficeAddress = item.ContactInfo.OfficeAddress;
                usr.ContactInfo.OfficePhone = item.ContactInfo.OfficePhone;
                usr.ContactInfo.EmailAdd = item.ContactInfo.EmailAdd;
                usr.ContactInfo.CellphoneNumber = item.ContactInfo.CellphoneNumber;

                userlst.Add(usr);

            }

            return userlst;
        }

        public List<Physician> GetAllSpecialization()
        {
            List<Physician> userlst = new List<Physician>();
            TestDBEntities tstDb = new TestDBEntities();
            var lstUsr = from k in tstDb.Physicians select k;
            foreach (var item in lstUsr)
            {
                Physician usr = new Physician();

                usr.Specialization.Name = item.Specialization.Description;
                usr.Specialization.Name = item.Specialization.Description;
                

                userlst.Add(usr);

            }

            return userlst;
        }





        public Physician GetAllUserById(int id)
        {

            TestDBEntities tstDb = new TestDBEntities();
            var lstUsr = from k in tstDb.Physicians where k.Id == id select k;
            Physician usr = new Physician();
            foreach (var item in lstUsr)
            {
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
                    CellphoneNumber = item.ContactInfo.CellphoneNumber
                };

                usr.Specialization = new Specialization
                {
                    Id = item.Id,
                    Name = item.Specialization.Name,
                    Description = item.Specialization.Description
                };


            }

            return usr;
        }

        public int DeleteUserById(int Id)
        {

            TestDBEntities tstDb = new TestDBEntities();
            Specialization usrspc = new Specialization();
            ContactInfo usrctn = new ContactInfo();
            Physician usrdtl = new Physician();
            usrspc.Id = Id;
            usrctn.Id = Id;
            usrdtl.Id = Id;
            tstDb.Entry(usrspc).State = EntityState.Deleted;
            tstDb.Entry(usrctn).State = EntityState.Deleted;
            tstDb.Entry(usrdtl).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int AddUser(int Id,string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, int? Weight, int? Height, string HomeAddress, Int64? HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64? CellphoneNumber, string Name, string Description)
        {

            TestDBEntities tstDb = new TestDBEntities();
            Physician usrdtl = new Physician();
            usrdtl.Id = Id;
            usrdtl.FirstName = FirstName;
            usrdtl.MiddleName = MiddleName;
            usrdtl.LastName = LastName;
            usrdtl.BirthDate = BirthDate;
            usrdtl.Gender = Gender;
            usrdtl.Weight = Weight;
            usrdtl.Height = Height;
            usrdtl.ContactInfo = new ContactInfo
            {
                Id = Id,
                HomeAddress = HomeAddress,
                HomePhone = HomePhone,
                OfficeAddress = OfficeAddress,
                OfficePhone = OfficePhone,
                EmailAdd = EmailAdd,
                CellphoneNumber = CellphoneNumber
            };
            usrdtl.Specialization = new Specialization
            {
                Id = Id,
                Name =Name,
                Description = Description
            };
            tstDb.Physicians.Add(usrdtl);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }
        public int AddContact (string HomeAddress, Int64 HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64 CellphoneNumber)
        {

            TestDBEntities tstDb = new TestDBEntities();
            Physician usrdtl = new Physician();
            usrdtl.ContactInfo.HomeAddress = HomeAddress;
            usrdtl.ContactInfo.HomePhone = HomePhone;
            usrdtl.ContactInfo.OfficeAddress = OfficeAddress;
            usrdtl.ContactInfo.OfficePhone = OfficePhone;
            usrdtl.ContactInfo.EmailAdd = EmailAdd;
            usrdtl.ContactInfo.CellphoneNumber = CellphoneNumber;
            tstDb.Physicians.Add(usrdtl);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int AddSpecialization(string Name, string Description)
        {

            TestDBEntities tstDb = new TestDBEntities();
            Physician usrdtl = new Physician();
            usrdtl.Specialization.Name = Name;
            usrdtl.Specialization.Description = Description;
            tstDb.Physicians.Add(usrdtl);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int UpdateUser(int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, int? Weight, int? Height, string HomeAddress, Int64? HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64? CellphoneNumber, string Name, string Description)
        {
            TestDBEntities tstDb = new TestDBEntities();
            Physician usrdtl = new Physician();
            
            
            usrdtl.Id = Id;
            usrdtl.FirstName = FirstName;
            usrdtl.MiddleName = MiddleName;
            usrdtl.LastName = LastName;
            usrdtl.BirthDate = BirthDate;
            usrdtl.Gender = Gender;
            usrdtl.Weight = Weight;
            usrdtl.Height = Height;
            ContactInfo usrctn = new ContactInfo();
            usrctn.Id = Id;
            usrctn.HomeAddress = HomeAddress;
            usrctn.HomePhone = HomePhone;
            usrctn.OfficeAddress = OfficeAddress;
            usrctn.OfficePhone = OfficePhone;
            usrctn.EmailAdd = EmailAdd;
            usrctn.CellphoneNumber = CellphoneNumber;
            Specialization usrspc = new Specialization();
            usrspc.Id = Id;
            usrspc.Name = Name;
            usrspc.Description = Description;

            tstDb.Entry(usrdtl).State = EntityState.Modified;
            tstDb.Entry(usrctn).State = EntityState.Modified;
            tstDb.Entry(usrspc).State = EntityState.Modified;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

    }
}
