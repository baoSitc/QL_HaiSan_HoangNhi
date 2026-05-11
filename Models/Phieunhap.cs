using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("PHIEUNHAP")]
public partial class Phieunhap
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("SOPHIEU")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Sophieu { get; set; }

    [Column("NGAYNHAP", TypeName = "datetime")]
    public DateTime? Ngaynhap { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("NHACUNGCAP_ID")]
    public int? NhacungcapId { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("TONGTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Tongtien { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [ForeignKey("NhacungcapId")]
    [InverseProperty("Phieunhaps")]
    public virtual Nhacungcap? Nhacungcap { get; set; }

    [ForeignKey("NhanvienId")]
    [InverseProperty("Phieunhaps")]
    public virtual Nhanvien? Nhanvien { get; set; }
}
