using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("PHIEUTHU")]
[Index("KhachhangId", Name = "IX_PHIEUTHU_KHACHHANG")]
public partial class Phieuthu
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("SOPHIEUTHU")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Sophieuthu { get; set; }

    [Column("NGAYTHU", TypeName = "datetime")]
    public DateTime? Ngaythu { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("KHACHHANG_ID")]
    public int? KhachhangId { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("SOTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Sotien { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [ForeignKey("KhachhangId")]
    [InverseProperty("Phieuthus")]
    public virtual Khachhang? Khachhang { get; set; }

    [ForeignKey("NhanvienId")]
    [InverseProperty("Phieuthus")]
    public virtual Nhanvien? Nhanvien { get; set; }
}
