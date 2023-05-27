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

        public async Task<long> AcceptOrRejectTheOrder(OrderDeliveryModel orderDeliveryModel)
        {

            var result = 0;
            Order order = new Order();
            orderDeliveryModel.ModifiedDate=DateTime.Now;
            orderDeliveryModel.IsAccepted=true;
            orderDeliveryModel.OrderStatusId=4;
            orderDeliveryModel.AcceptedDate = DateTime.Now;

            order.modifiedBy = orderDeliveryModel.modifiedBy;
            order.ModifiedDate=DateTime.Now;
            order.OrderStatusId = 4;
            var query = @"Update tblDeliveryBoyOrderStatus set OrderStatusId=@OrderStatusId,
           IsAccepted=@IsAccepted,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,AcceptedDate=@AcceptedDate where OrderId=@OrderId";
            var queryupdateOrder = @"Update  tblOrder Set OrderStatusId=@OrderStatusId,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate
             Where Id=@Id";
            using (var connection = dapperContext.CreateConnection())
            {
               
                var orderids = await connection.QueryAsync<long>(@"select OrderId from tblDeliveryBoyOrderStatus 
                         where OrderStatusId=3 and DeliveryBoyId=@DeliveryBoyId", new { DeliveryBoyId = orderDeliveryModel.DeliveryBoyId});
                foreach(var orderid in orderids) 
                {
                    try
                    {
                        orderDeliveryModel.OrderId = orderid;
                        order.Id = orderid;
                        result = await connection.ExecuteAsync(query, orderDeliveryModel);
                        var result1 = await connection.ExecuteAsync(queryupdateOrder, order);
                    }
                    catch (Exception ex)
                    {

                    }
                  
                   
                }
                return result;
            }
           
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

        public async Task<List<GetOrderDelliveryBoy>> GetOrderAcceptedByDeliveryBoy(long DelieryBoyId)
        {
            var query = @"select o.Id as OrderId,p.ProductName,o.ShippingAddress,
                             d.DistrictName,sub.Sub_DivisionsName,ds.AcceptedDate,
                             o.CreatedDate from tblOrder o 
                            inner join tblProducts p on o.ProductId=p.Id
                            inner join tblDeliveryBoyOrderStatus ds on o.Id=ds.OrderId 
                            inner join tblDistrict d on o.DistrictId=d.Id
                            inner join tblSub_Divisions sub on o.Sub_DivisionId=sub.Id
                            where IsAccepted=1 and ds.DeliveryBoyId=@DeliveryBoyId and o.OrderStatusId=4";
            using(var connection=dapperContext.CreateConnection())
            {
                var orders=await connection.QueryAsync<GetOrderDelliveryBoy>(query, new {DeliveryBoyId=DelieryBoyId});
                return orders.ToList();
            }
     
        }

        public async Task<long> OrderDeliveryredToCustomer(OrderDeliveryModel orderDelivery)
        {
            Order order = new Order();
            orderDelivery.ModifiedDate= DateTime.Now;   
            order.Id= orderDelivery.OrderId;
            order.modifiedBy = orderDelivery.modifiedBy;
            order.ModifiedDate = DateTime.Now;
            var queryupdelivery = @"update tblDeliveryBoyOrderStatus set OrderStatusId=5,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate
                        where OrderId=@OrderId";
            var queryuporder = @"update tblOrder set OrderStatusId=5,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate
                             where Id=@Id";
            using(var connection=dapperContext.CreateConnection())
            {
                var result1=await connection.ExecuteAsync(queryupdelivery, orderDelivery);
                var result2=await connection.ExecuteAsync(queryuporder, order);
                return result1 + result2;   
            }
        }
    }
}
