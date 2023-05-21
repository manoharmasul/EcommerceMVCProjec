using System.Collections.Generic;

namespace EcommerceProject.Models
{
    public class User:BaseModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public double WalletBalance { get; set; }
    }
    public class GetAllUserModelManagerOther
    {
        public List<UserRegistrationModel> managerlist { get; set; }
        public List<UserRegistrationModel> allemployeelist { get; set; }
    }
    public class UserRegistrationModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }    
        public long RoleId { get; set; }
        public string Role { get; set; }
        public double WalletBalance { get; set; }
        public List<UserTypes> userTypes { get; set; }
    }
    public class UserTypes
    {
        public long RoleId { get; set; }
        public string RoleType { get; set; }
    }
    public class UserLogInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserPasswordModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
    public class WalletBalanceClass
    {
        public long Id { get; set; }
        public double WalletBalance { get; set; }
    }

    public class GetUserLogInModel 
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public double WalletBalance { get; set; }
    }
    public class AssignLocationModel:BaseModel
    {
       // insert into tblAssignLocation(SalesMagerId, DistrictId, CreatedBy, CreatedDate, IsDeleted) values(@SalesMagerId, @DistrictId, @CreatedBy, @CreatedDate,0)

        public long SalesMagerId { get; set; }
        public long DistrictId { get; set; }
        public long resultreturn { get; set; }
        public List<DistrictModel> distlist { get; set; }
    }
}
