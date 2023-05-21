namespace EcommerceProject.Repository.Interface
{
    public interface IOrderDeliveryBoy
    {
        Task<long> AssingOrAcceptOrderDelivery();
    }
}
