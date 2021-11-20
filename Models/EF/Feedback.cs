namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Họ Tên")]
        public string Name { get; set; }
       
        [Display(Name = "Số Điện Thoại")]
        [StringLength(50)]
        public string Phone { get; set; }
        
        [StringLength(50)]
        public string Email { get; set; }
        
        [Display(Name = "Địa Chỉ")]
        [StringLength(50)]
        public string Address { get; set; }
        
        [Display(Name = "Nội Dung")]
        [StringLength(250)]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
       
    }
}
