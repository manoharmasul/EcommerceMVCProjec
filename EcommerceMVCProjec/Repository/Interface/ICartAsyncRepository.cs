using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface ICartAsyncRepository
    {
        Task<long> AddToCart(Cart cartadd);
        Task<long> UpdateCart(Cart cartadd);
        Task<List<ProductAdd>> GetCartProducts(long userId);
        Task<long> RemoveFromCart(long productid,long userId);
    }
}
