using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Models
{
    public class BaseModel
    {
        [Required(ErrorMessage = "Created by is required")]
        public long createdBy { get; set; }
        [Required(ErrorMessage = "Modified by is required")]
        public long modifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }


        public class DeleteObj
        {
            public int Id { get; set; }
            public int ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }

        }
    }
}
