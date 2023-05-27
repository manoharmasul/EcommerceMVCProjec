using EcommerceProject.Models;

namespace EcommerceMVCProjec.Repository.Interface
{
    public interface IOrderDeliveryBoyRepository
    {
        Task<long> AssignOrderToDeliveryBoy(OrderDeliveryModel orderDeliveryModel);
        Task<long> AcceptOrRejectTheOrder(OrderDeliveryModel orderDeliveryModel);
        Task<long> OrderDeliveryredToCustomer(OrderDeliveryModel orderDelivery);
        Task<List<GetOrderDelliveryBoy>> GetOrderAcceptedByDeliveryBoy(long DelieryBoyId);
    }
}
