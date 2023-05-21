using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IAttendaceRepository
    {
        Task<long> CheckInOut(AttendanceModel attendance);
        Task<List<GetAttendanceModel>> GetAllAttendancesByEmpId(string? UserName,DateTime? FromDate,DateTime? ToDate);
    }
}
