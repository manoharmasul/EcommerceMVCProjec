using Dapper;
using EcommerceMVCProjec.Repository.Interface;
using EcommerceProject.Context;
using EcommerceProject.Models;

namespace EcommerceMVCProjec.Repository
{
    public class OrderDeliveryBoyRepository:IOrderDeliveryBoyRepository
    {
        private readonly DapperContext dapperContext;
        public OrderDeliveryBoyRepository(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<long> AssignOrderToDeliveryBoy(OrderDeliveryModel orderDeliveryModel)
        {
            var result = 0;
            var query = @"insert into tblDeliveryBoyOrderStatus
                       (DeliveryBoyId,OrderId,OrderStatusId,CreatedBy,CreatedDate,IsDeleted)
                       Values(@DeliveryBoyId,@OrderId,@OrderStatusId,@CreatedBy,@CreatedDate,0)";

            var uporderquery = @"Update tblOrder Set OrderStatusId=3,
            ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate where Id=@Id";

            Order order = new Order();
            order.modifiedBy = orderDeliveryModel.createdBy;
            order.ModifiedDate = DateTime.Now;
            orderDeliveryModel.CreatedDate = DateTime.Now;
            orderDeliveryModel.OrderStatusId = 3;
           
            using (var connection=dapperContext.CreateConnection())
            {
                var orderids = await connection.QueryAsync<long>(@"select Id from tblOrder where Sub_DivisionId=@Sub_DivisionId and OrderStatusId=2", new {Sub_DivisionId=orderDeliveryModel.Sub_DivisionId});
                foreach (var orderid in orderids)
                {
                    orderDeliveryModel.OrderId=orderid;
                    order.Id = orderid;
                     result = await connection.ExecuteAsync(query, orderDeliveryModel);
                    var orderresult = await connection.ExecuteAsync(uporderquery, order);
                }
                return result;

            }
        }
    }
}
