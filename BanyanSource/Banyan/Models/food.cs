﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Banyan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class food
    {
        public food()
        {
            this.billingdetail = new HashSet<billingdetail>();
            this.specialdate = new HashSet<specialdate>();
        }

        public System.Guid id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public System.Guid categoryid { get; set; }
        [Display(Name = "Tên món")]
        [Required(ErrorMessage = "Vui lòng điền tên món")]
        public string name { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng điền giá")]
        public decimal price { get; set; }
        [Display(Name = "Bán mọi ngày")]
        public bool allday { get; set; }
        [Display(Name = "Được bán")]
        public bool enable { get; set; }
        public Nullable<int> inventory { get; set; }
        public Nullable<int> sold { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> createdate { get; set; }
        [Display(Name = "Hình lớn")]
        public string bigimage { get; set; }
        [Display(Name = "Hình nhỏ")]
        public string smallimage { get; set; }
        [Display(Name = "Vị trí")]
        public Nullable<int> position { get; set; }

        public virtual ICollection<billingdetail> billingdetail { get; set; }
        public virtual category category { get; set; }
        public virtual ICollection<specialdate> specialdate { get; set; }
    }
}
