using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("PHIEUXUATKHO")]
public partial class Phieuxuatkho
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("SOPHIEU")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Sophieu { get; set; }

    [Column("NGAYXUAT", TypeName = "datetime")]
    public DateTime? Ngayxuat { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("LYDO")]
    [StringLength(500)]
    public string? Lydo { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [InverseProperty("Phieuxuatkho")]
    public virtual ICollection<CtPhieuxuatkho> CtPhieuxuatkhos { get; set; } = new List<CtPhieuxuatkho>();

    [ForeignKey("NhanvienId")]
    [InverseProperty("Phieuxuatkhos")]
    public virtual Nhanvien? Nhanvien { get; set; }
}
