//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLShopThoiTrang.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhapHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhapHang()
        {
            this.ChiTietNhaps = new HashSet<ChiTietNhap>();
        }
    
        public string MaDonNhap { get; set; }
        public string MaNCC { get; set; }
        public string MaNV { get; set; }
        public System.DateTime NgayNhap { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietNhap> ChiTietNhaps { get; set; }
        public virtual NCC NCC { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
