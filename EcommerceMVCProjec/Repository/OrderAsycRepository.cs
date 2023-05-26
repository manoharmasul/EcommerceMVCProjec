using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace EcommerceProject.Repository
{
    public class OrderAsycRepository : IOrderAsyncRepository
    {
        private readonly DapperContext context;
        public OrderAsycRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<List<GetAllOrdersForAdmin>> GetAllOrders(long? SalesManagerId, long? rolecheckId)
        {
            string query;
            List<GetAllOrdersForAdmin> alladminorderlist = new List<GetAllOrdersForAdmin>();
            if (rolecheckId == 0)
            {
                //SrNo,ProductId,ProductName,OrderCounts,OrderStatus,OrderQuantity,Available_Products
                 query = @"if(@SalesManagerId=0)
                            begin
	                          select   ROW_NUMBER() OVER (
                            Order By Os.OrderStatus
                            ) SrNo,
                            p.Id as ProductId,p.ProductName,Count(p.Id) as OrderCounts,
                            Os.OrderStatus,Os.Id,(select sum(Quantity) from tblOrder where ProductId=p.Id) 
                            as OrderQuantity,p.ProductQuantity as Available_Products from tblOrder o
                            inner join tblProducts p on o.ProductId=p.Id
                            inner join tblorderStatus os on o.OrderStatusId=os.Id
                            where   o.IsDeleted=0 and o.OrderStatusId=1 group by p.Id,p.ProductName,
                            Os.OrderStatus,p.ProductQuantity,Os.Id  Order by Os.Id
                            end 
                            else 
                            begin 
                                select ROW_NUMBER() OVER (
                                order by  sub.Id
                                ) SrNo,Sub_DivisionsName,count(O.Id) as OrderCounts,o.Sub_DivisionId,os.OrderStatus from tblOrder o 
                                inner join tblSub_Divisions sub on o.Sub_DivisionId=sub.Id
                                inner join tblOrderStatus os on o.OrderStatusId=os.Id
                                where Sub.DistrictId=(Select DistrictId from tblAssignLocation 
                                where SalesManagerId=@SalesManagerId) and o.OrderStatusId=2
                                Group by Sub_DivisionId,Sub_DivisionsName,sub.Id,os.OrderStatus
                            end
                     ";
            }
            else
            {
                 query = @"	select  ROW_NUMBER() OVER (
	                order by  p.Id) SrNo,p.Id as ProductId,p.ProductName,
	                sum(o.TotalAmmount) as TotalAmmount,o.Quantity as OrderQuantity,sub.Sub_DivisionsName,ost.OrderStatus
	                from tblOrder o
	                inner join tblProducts p on o.ProductId=p.Id
	                inner join tblDeliveryBoyOrderStatus os on o.Id=os.OrderId 
	                inner join tblSub_Divisions sub on o.Sub_DivisionId=sub.Id
	                inner join tblOrderStatus ost on o.OrderStatusId=ost.Id
	                where os.DeliveryBoyId=@DeliveryBoyId and o.OrderStatusId=3 
	                group by p.Id,p.ProductName,TotalAmmount,o.Quantity,sub.Sub_DivisionsName,ost.OrderStatus
                     ";
            }
            using (var connection = context.CreateConnection())
            {
                if (rolecheckId == 0)
                {
                    var orders = await connection.QueryAsync<GetAllOrdersForAdmin>(query, new { SalesManagerId = SalesManagerId });

                    var deliveryboys = await connection.QueryAsync<DeliveryBoyListModel>
                       (@"Select u.Id as DeliveryBoyId,u.UserName,
                    sub.Id as Sub_DivisionId 
                    from tblUser u 
                    inner join tblAssignDeliveryBoyTeam adbt on u.Id=adbt.DeliveryBoyId
                     inner join tblSub_Divisions sub on adbt.Sub_DivisionId=sub.Id");



                    foreach (var order in orders)
                    {
                        order.RoleChekcId = 0;
                        order.DeliveryBoys = deliveryboys.ToList();

                    }
                    
                    return orders.ToList();
                }
                else
                {

                    var orders = await connection.QueryAsync<GetAllOrdersForAdmin>(query, new { DeliveryBoyId = SalesManagerId });
                    foreach (var order in orders)
                    {
                        order.DeliveryBoyId = (long)SalesManagerId;
                        order.RoleChekcId = (long)rolecheckId;
                       break;

                    }
                    return orders.ToList();
                }
              
            }
        }

        /* public async Task<AddOrderItems> GetDistrictAndSubDistrict()
         {
             AddOrderItems addOrderItems = new AddOrderItems();
             var querydist = @"select * from tblDistrict";
             var querysubdiv = @"Select * from tblSub_Divisions";

             using (var connection = context.CreateConnection())
             {
                 var distlist = await connection.QueryAsync<DistrictModel>(querydist);

                 var subdiv = await connection.QueryAsync<SubDivisionModel>(querysubdiv);
                 addOrderItems.districtModels=distlist.ToList();
                 addOrderItems.subDivisionModel=subdiv.ToList();
                 return addOrderItems;
             }
         }*/

        public async Task<List<GetOrder>> GetMyOrders(long userid)



        {
            var query = @"select o.Id,o.OrderStatusId,p.ImageUrl,p.ProductName,o.ShippingAddress,os.OrderStatus,
                        o.BillingAddress,o.CreatedDate as OrderDate,
                        o.MobileNo,o.TotalAmmount from tblOrder o
                        inner join tblProducts p on o.ProductId=p.Id 
                        inner join tblOrderStatus os on o.OrderStatusId=os.Id
                        where CustomerId=@UserId and o.IsDeleted=0 order by o.OrderStatusId ";
            using (var connection = context.CreateConnection())
            {
                var orders = await connection.QueryAsync<GetOrder>(query, new { UserId = userid });
                return orders.ToList();
            }
        }

        public async Task<Order> GetOrderById(long id)
        {
            var query = @"Select * From tblOrder Where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<Order>(query, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<UpdateOrderBillingAddress> GetOrdersForAdminUpdate(long? SalesManagerId, long? Sub_DivisionId, long productId)
        {
            var query = @"select   
                p.Id as ProductId,p.ProductName,Count(p.Id) as OrderCounts,
                Os.OrderStatus,(select sum(Quantity) from tblOrder where ProductId=p.Id) 
                as OrderQuantity,p.ProductQuantity as Available_Products from tblOrder o
                inner join tblProducts p on o.ProductId=p.Id
                inner join tblorderStatus os on o.OrderStatusId=os.Id
                where   o.IsDeleted=0 and p.Id=@ProductId 
                group by p.Id,p.ProductName,Os.OrderStatus,p.ProductQuantity";
            using (var connection = context.CreateConnection())
            {
                if (Sub_DivisionId != 0)
                {
                    var DeliveryBoys = await connection.QueryAsync<DeliveryBoyListModel>(@"select u.Id,adbt.Sub_DivisionId,u.UserName from tblUser u
                                                                inner join tblAssignDeliveryBoyTeam adbt on  u.Id=adbt.DeliveryBoyId
                                                                where adbt.Sub_DivisionId=@Sub_DivisionId
                                                                ", new { Sub_Divisionid = Sub_DivisionId });
                    var result = await connection.QueryFirstOrDefaultAsync<UpdateOrderBillingAddress>(query, new { ProductId = productId });
                    result.DeliveryBoyList = DeliveryBoys.ToList();
                    result.CheckId = 1;
                    return result;
                }
                var result1 = await connection.QueryFirstOrDefaultAsync<UpdateOrderBillingAddress>(query, new { ProductId = productId });
                return result1;
            }
        }

        public async Task<long> OrdreItem(Order order)
        {
            var query = @"insert into tblOrder(CustomerId,ProductId,TotalAmmount,OrderStatusId,BillingAddress,

                         ShippingAddress,CreatedBy,CreatedDate,IsDeleted,MobileNo,Quantity)

                         values (@CustomerId,@ProductId,@TotalAmmount,@OrderStatusId,@BillingAddress,

                         @ShippingAddress,@CreatedBy,@CreatedDate,0,@MobileNo,@Quantity)";

            using (var connection = context.CreateConnection())
            {
                order.CreatedDate = DateTime.Now;

                order.OrderStatusId = 1;

                var AvailableQuantity = await connection.QueryFirstOrDefaultAsync<long>

                    (@"select ProductQuantity from tblProducts where Id=@ProductId",

                    new { ProductId = order.ProductId });
                AvailableQuantity = AvailableQuantity - order.Quantity;

                var queryupProduct = await connection.ExecuteAsync

                (@"Update tblProducts set ProductQuantity=@ProductQuantity where Id=@ProductId",

                new { ProductQuantity = AvailableQuantity, ProductId = order.ProductId });


                var result = await connection.ExecuteAsync(query, order);
                return result;
            }
        }

        public async Task<AddOrderItems> OrdreItems(AddOrderItems order)
        {

            order.CreatedDate = DateTime.Now;
            AddOrderItems addOrderItems = new AddOrderItems();
            var query = @"insert into tblOrder(CustomerId,ProductId,TotalAmmount,OrderStatusId,BillingAddress,

                         ShippingAddress,CreatedBy,CreatedDate,IsDeleted,MobileNo,Quantity,DistrictId,Sub_DivisionId)

                         values (@CustomerId,@ProductId,@TotalAmmount,1,@BillingAddress,

                         @ShippingAddress,@CreatedBy,@CreatedDate,0,@MobileNo,@Quantity,@DistrictId,@Sub_DivisionId)";
            var querydist = @"select * from tblDistrict";
            var querysubdiv = @"Select * from tblSub_Divisions";

            using (var connection = context.CreateConnection())
            {
                // addOrderItems.OrderStatusId=1;


                var distId = await connection.QueryAsync<long>(@"select * from tblDistrict where DistrictName=@DistrictName", new { DistrictName = order.DistrictName });

                order.DistrictId = distId.FirstOrDefault();
                var distlist = await connection.QueryAsync<DistrictModel>(querydist);

                var subdiv = await connection.QueryAsync<SubDivisionModel>(querysubdiv);
                addOrderItems.subDivisionModel = subdiv.ToList();
                addOrderItems.districtModels = distlist.ToList();
                if (order.resultreturn == 10)
                {
                    return addOrderItems;

                }
                try
                {
                    order.resultreturn = await connection.ExecuteAsync(query, order);
                }
                catch (Exception ex)
                {

                    throw;
                }


                return order;
            }
        }

        public async Task<long> UpdateOrderStatuss(UpdateOrderStatus updateordstatus)
        {
            var query = @"Update tblOrder Set OrderStatusId=@OrderStatusId where Id=@Id";

            using (var connection = context.CreateConnection())
            {
                updateordstatus.OrderStatusId = 4;
                var result = await connection.ExecuteAsync(query, updateordstatus);

                return result;
            }
        }

        public async Task<long> UpdateOrdre(UpdateOrderBillingAddress order)
        {
            order.ModifiedDate = DateTime.Now;
            var query = @"update tblOrder set BillingAddress=@BillingAddress,	
                        OrderStatusId=@OrderStatusId,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate
                        where ProductId=@ProductId ;";

            using (var connection = context.CreateConnection())
            {


                var result = await connection.ExecuteAsync(query,

                new
                {
                    OrderStatusId = order.OrderStatusId,
                    ModifiedBy = order.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    ProductId = order.ProductId,
                    BillingAddress = order.BillingAddress
                });

                return result;

            }
        }

        public async Task<long> UpdateOrdreByCustomer(long Id, long ModifiedBy)
        {
            var query = @"update  tblOrder set OrderStatus='Delivered',ModifiedBy=@ModifiedBy,ModifiedDate=Getdate(),DeliveryDate=getDate() where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { Id = Id, ModifiedBy = ModifiedBy });
                return result;
            }
        }
    }
}
