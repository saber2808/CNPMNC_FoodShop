//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeAnCNPMNC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            this.Bills = new HashSet<Bill>();
            ImageCoupon = "~/Content/Images/Avatar.jpg";
        }
        [Display(Name = "M� gi?m gi�: ")]
        public string IDCoupon { get; set; }
        [Display(Name = "T�n gi?m gi�: ")]
        public string NameCoupon { get; set; }
        [Display(Name = "H�nh ?nh gi?m gi�: ")]
        public string ImageCoupon { get; set; }
        [Display(Name = "Gi� gi?m gi�: ")]
        public Nullable<double> discount { get; set; }
        [Display(Name = "M� t? chi ti?t:")]
        public string Detail { get; set; }
        [Display(Name = "Ng�y b?t ??u: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> DateStart { get; set; }
        [Display(Name = "Ng�y k?t th�c: ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> DateEnd { get; set; }
        [Display(Name = "S? l??ng: ")]
        public Nullable<int> Quantity { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
    }
}