using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IOrderAsyncRepository
    {
        Task<long> OrdreItem(Order order);
     //   Task<AddOrderItems> GetDistrictAndSubDistrict();
        Task<AddOrderItems> OrdreItems(AddOrderItems order);
        Task<long> UpdateOrdre(UpdateOrderBillingAddress order);
        Task<long> UpdateOrdreByCustomer(long Id,long ModifiedBy);
        Task<long> UpdateOrderStatuss(UpdateOrderStatus updateordstatus);
        Task<List<GetOrder>> GetMyOrders(long userid);
        Task<List<GetAllOrdersForAdmin>> GetAllOrders(long SalesManagerId);
        Task<Order> GetOrderById(long id);
        Task<UpdateOrderBillingAddress> GetOrdersForAdminUpdate(long productId);

    }
}
