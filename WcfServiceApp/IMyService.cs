using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        List<Physician> GetAllPersonalInfo();

        [OperationContract]
        List<Physician> GetAllContactInfo();

        [OperationContract]
        List<Physician> GetAllSpecialization();

        [OperationContract]
        int AddUser(int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, int? Weight, int? Height, string HomeAddress, Int64? HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64? CellphoneNumber, string Name, string Description);

        [OperationContract]
        int AddContact(string HomeAddress, Int64 HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64 CellphoneNumber);

        [OperationContract]
        int AddSpecialization(string Name, string Description);

        [OperationContract]
        Physician GetAllUserById(int id);

        [OperationContract]
        int UpdateUser(int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, int? Weight, int? Height, string HomeAddress, Int64? HomePhone, string OfficeAddress, Int64 OfficePhone, string EmailAdd, Int64? CellphoneNumber, string Name, string Description);

        [OperationContract]
        int DeleteUserById(int Id);
    }
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }


    }
}
