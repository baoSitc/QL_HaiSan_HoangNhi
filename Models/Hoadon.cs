using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("HOADON")]
[Index("KhachhangId", Name = "IX_HOADON_KHACHHANG")]
[Index("Ngaylap", Name = "IX_HOADON_NGAYLAP")]
public partial class Hoadon
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("SOHD")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Sohd { get; set; }

    [Column("NGAYLAP", TypeName = "datetime")]
    public DateTime? Ngaylap { get; set; }

    [Column("KHACHHANG_ID")]
    public int? KhachhangId { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("SHIPPER_ID")]
    public int? ShipperId { get; set; }

    [Column("TONGTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Tongtien { get; set; }

    [Column("GIAMGIA", TypeName = "decimal(18, 2)")]
    public decimal? Giamgia { get; set; }

    [Column("THANHTOAN", TypeName = "decimal(18, 2)")]
    public decimal? Thanhtoan { get; set; }

    [Column("CONLAI", TypeName = "decimal(18, 2)")]
    public decimal? Conlai { get; set; }

    [Column("PHISHIP", TypeName = "decimal(18, 2)")]
    public decimal? Phiship { get; set; }

    [Column("PHIGUIXE", TypeName = "decimal(18, 2)")]
    public decimal? Phiguixe { get; set; }

    [Column("HINHTHUC_THANHTOAN")]
    [StringLength(100)]
    public string? HinhthucThanhtoan { get; set; }

    [Column("TRANGTHAI")]
    [StringLength(100)]
    public string? Trangthai { get; set; }

    [Column("TrangThaiThanhToan")]
    [StringLength(100)]
    public string? TrangThaiThanhToan { get; set; }

    
    [Column("DIACHIGIAO")]
    [StringLength(500)]
    public string? Diachigiao { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [InverseProperty("Hoadon")]
    public virtual ICollection<CtHoadon> CtHoadons { get; set; } = new List<CtHoadon>();

    [ForeignKey("KhachhangId")]
    [InverseProperty("Hoadons")]
    public virtual Khachhang? Khachhang { get; set; }

    [ForeignKey("NhanvienId")]
    [InverseProperty("Hoadons")]
    public virtual Nhanvien? Nhanvien { get; set; }

    [ForeignKey("ShipperId")]
    [InverseProperty("Hoadons")]
    public virtual Shipper? Shipper { get; set; }
}
