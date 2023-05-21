namespace EcommerceProject.Models
{
    public class AttendanceModel
    {
        //Id,EmpId,AttendanceDate,InTime,OutTime,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long EmpId { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class GetAttendanceModel
    {
        //Id,EmpId,AttendanceDate,InTime,OutTime,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public string? Time { get; set; }
        public string? AttendanceDate { get; set; }
        public string UserName { get; set; }
        public string Days { get; set; }
        public List<EmployeeLists> emplist { get; set; }
    }
  
    public class EmployeeLists
    {
        //Id,EmpId,AttendanceDate,InTime,OutTime,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public string UserName { get; set; }
    }
}
