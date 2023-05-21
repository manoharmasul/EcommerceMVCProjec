using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using System.Diagnostics;


namespace EcommerceProject.Repository
{
    public class AttendanceRepository : IAttendaceRepository
    {
        private readonly DapperContext context;
        public AttendanceRepository(DapperContext context)
        {
            this.context = context;
        }
        public async Task<long> CheckInOut(AttendanceModel attendance)
        {
            var result = 0;
            //Id,EmpId,AttendanceDate,InTime,OutTime,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
            var queryCheckIn = @"insert into tblAttendance(EmpId,AttendanceDate,InTime,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted) 
                         values (@EmpId,@AttendanceDate,@InTime,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,0)";
            var queryCheckOutt = @"Update tblAttendance set OutTime=@OutTime,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate where EmpId=@EmpId and CONVERT(date,AttendanceDate)=CONVERT(date,@AttendanceDate) and OutTime is null";
            var queryCheckInn = @"Update tblAttendance set InTime=@InTime,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate where EmpId=@EmpId and CONVERT(date,AttendanceDate)=CONVERT(date,@AttendanceDate) and OutTime is null";

            using (var con = context.CreateConnection())
            {
                attendance.AttendanceDate = DateTime.Now;
                attendance.CreatedDate = DateTime.Now;
                attendance.ModifiedDate = DateTime.Now;
                attendance.InTime = DateTime.Now;
                attendance.OutTime=DateTime.Now;
                attendance.IsDeleted = false;
                var dateToday = DateTime.Now;
             
                var checkoutin = await con.QuerySingleOrDefaultAsync<AttendanceModel>(@"select TOP 1 * from tblAttendance where CONVERT(date,AttendanceDate)=CONVERT(date,@AttendanceDate) and EmpId=@EmpId ORDER BY Id DESC ", new { AttendanceDate = attendance.AttendanceDate,EmpId=attendance.EmpId });                  
            
                if (checkoutin == null || checkoutin.InTime == null)
                {
                     result = await con.ExecuteAsync(queryCheckIn, attendance);
                    return result;
                }
                else if (checkoutin.InTime != null &&  checkoutin.OutTime==null )
                {
                     result = await con.ExecuteAsync(queryCheckOutt, attendance);
                    return result;
                }
                else
                {
                    result = await con.ExecuteAsync(queryCheckIn, attendance);
                    return result;

                }
               

            }
        }

        public async Task<List<GetAttendanceModel>> GetAllAttendancesByEmpId(string? UserName, DateTime? FromDate, DateTime? ToDate)
        {
            long EmpId = 0;
    
             if(FromDate==null)
             {
                    FromDate  = DateTime.ParseExact("2023-01-01 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
             }
             if(ToDate==null)
             {
                ToDate = DateTime.Now;
             }


            var query = @"
                        SELECT UserName,CONVERT(varchar, (sum(DATEDIFF(SECOND, intime, outtime)) / 86400))   
                        + ':' + CONVERT(varchar, DATEADD(ss, sum(DATEDIFF(SECOND, intime, outtime)), 0), 108) 
                        as Time,cast(AttendanceDate as Date) as AttendanceDate,EmpId from tblAttendance 
                        inner join tblUser on tblAttendance.EmpId=tblUser.Id
                        where tblUser.RoleId !=5 and (EmpId=@EmpId or @EmpId=0) and Cast(AttendanceDate as Date) between Cast(@fDate as Date) 
                        and Cast(@tDate as Date)  group by cast(AttendanceDate as date),EmpId,UserName";
            using(var con=context.CreateConnection())
            {

                if (UserName != null)
                {
                    EmpId = await con.QuerySingleOrDefaultAsync<long>(@"select Id From tblUser where UserName=@UserName and IsDeleted=0", new { UserName = UserName });
                }

                var result = await con.QueryAsync<GetAttendanceModel>(query, new { fDate = FromDate, tDate = ToDate, EmpId = EmpId });
                result.ToList();
                if (result.Count()>0)
                {


                    var getgetlist = await con.QueryAsync<EmployeeLists>(@"Select * From tblUser where IsDeleted=0");
                    foreach (var item in result)
                    {
                        item.emplist = getgetlist.ToList();
                        break;
                    }
                }
                return result.ToList();            
             }

        }
    }
}
