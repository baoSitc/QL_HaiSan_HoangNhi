using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("KHACHHANG")]
[Index("Sdt", Name = "IX_KHACHHANG")]
[Index("Makh", Name = "UQ__KHACHHAN__603F592D173876EA", IsUnique = true)]
public partial class Khachhang
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MAKH")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Makh { get; set; }

    [Column("TENKH")]
    [StringLength(300)]
    public string? Tenkh { get; set; }

    [Column("SDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [Column("DIACHI")]
    [StringLength(500)]
    public string? Diachi { get; set; }

    [Column("CONGNO", TypeName = "decimal(18, 2)")]
    public decimal? Congno { get; set; }

    [Column("HANMUCNO", TypeName = "decimal(18, 2)")]
    public decimal? Hanmucno { get; set; }

    [Column("DANGHOATDONG")]
    public bool? Danghoatdong { get; set; }

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [Column("ZALO")]
    [StringLength(100)]
    public string? Zalo { get; set; }

    [InverseProperty("Khachhang")]
    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    [InverseProperty("Khachhang")]
    public virtual ICollection<Phieuthu> Phieuthus { get; set; } = new List<Phieuthu>();
}
