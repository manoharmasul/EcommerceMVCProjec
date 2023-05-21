using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class CartAsyncRepository:ICartAsyncRepository
    {
        private readonly DapperContext context;
        public CartAsyncRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<long> AddToCart(Cart cartadd)
        {
            
            // Id,ProductId,UserId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

            var query = @"insert into tblCart (ProductId,UserId,CreatedBy,CreatedDate,IsDeleted) 
                        values(@ProductId,@UserId,@CreatedBy,GetDate(),0)";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, cartadd);
                return result;
            }

        }

        public async Task<List<ProductAdd>> GetCartProducts(long userId)
        {
            var query = @"select * from tblCart c inner join  tblProducts p on c.ProductId=p.Id where c.UserId=@UserId";
            using(var connection = context.CreateConnection())
            {
                var result=await connection.QueryAsync<ProductAdd>(query,new {UserId=userId });
                return result.ToList();
            }

        }

        public Task<long> RemoveFromCart(long productid, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateCart(Cart cartadd)
        {
            throw new NotImplementedException();
        }
    }
}
