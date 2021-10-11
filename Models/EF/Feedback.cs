﻿namespace Models.EF
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
        [Required(ErrorMessage = "Yêu Cầu Nhập Số Điện Thoại")]
        [Display(Name = "Số Điện Thoại")]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Yêu Cầu nhập Địa Chỉ")]
        [Display(Name = "Địa Chỉ")]
        [StringLength(50)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Nội Dung")]
        [Display(Name = "Nội Dung")]
        [StringLength(250,MinimumLength =20, ErrorMessage = "Độ dài Yêu Cầu ít nhất 20 ký tự.")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
