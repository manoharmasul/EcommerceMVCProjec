using EcommerceProject.Models;

namespace EcommerceMVCProjec.Repository.Interface
{
    public interface IOrderDeliveryBoyRepository
    {
        Task<long> AssignOrderToDeliveryBoy(OrderDeliveryModel orderDeliveryModel);
    }
}
