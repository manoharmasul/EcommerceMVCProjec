using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(UserRegistrationModel userregistration);
        Task<long> AddEmployee(UserRegistrationModel userregistration);
        Task<long> EditUser(UserRegistrationModel userregistration);
        Task<long> SetPassword(UserPasswordModel userpassoword);
        Task<GetAllUserModelManagerOther> GetAllUserModelManagerOther();
        Task<List<UserRegistrationModel>> GetAllUsers();
        Task<UserRegistrationModel> GetAllUsersAdd();
        Task<List<GetNetworkSalesManagerModel>> GetNetworkBySalesManager(long? salesmagerId);
        Task<UserRegistrationModel> GetUserById(long id);
        Task<GetUserLogInModel> UserLogIn(UserLogInModel usermodel);
        Task<long> AddWalletBalance(UserRegistrationModel addwalletbalace);
        Task<long> AssignLocationsToManager(AssignLocationModel assignLocation);
        Task<long> AssigLocationToDeliveryBoy(AssiginDeliveryBoyModel assigndeliveryboymodel);
        

    }
}
