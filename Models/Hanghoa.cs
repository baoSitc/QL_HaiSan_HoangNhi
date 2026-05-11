using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("HANGHOA")]
[Index("Tenhh", Name = "IX_HANGHOA_TENHH")]
[Index("Mahh", Name = "UQ__HANGHOA__603F20C3060DEAE8", IsUnique = true)]
public partial class Hanghoa
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MAHH")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Mahh { get; set; }

    [Column("TENHH")]
    [StringLength(300)]
    public string? Tenhh { get; set; }

    [Column("LOAIHANG_ID")]
    public int? LoaihangId { get; set; }

    [Column("DVT")]
    [StringLength(50)]
    public string? Dvt { get; set; }

    [Column("GIANHAP", TypeName = "decimal(18, 2)")]
    public decimal? Gianhap { get; set; }

    [Column("GIABAN", TypeName = "decimal(18, 2)")]
    public decimal? Giaban { get; set; }

    [Column("TONKHO", TypeName = "decimal(18, 2)")]
    public decimal? Tonkho { get; set; }

    [Column("DINHMUCTON", TypeName = "decimal(18, 2)")]
    public decimal? Dinhmucton { get; set; }

    [Column("DANGKINHDOANH")]
    public bool? Dangkinhdoanh { get; set; }

    [Column("HINHANH")]
    [StringLength(500)]
    public string? Hinhanh { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [Column("CREATED_AT", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Hanghoa")]
    public virtual ICollection<CtHoadon> CtHoadons { get; set; } = new List<CtHoadon>();

    [InverseProperty("Hanghoa")]
    public virtual ICollection<CtPhieuxuatkho> CtPhieuxuatkhos { get; set; } = new List<CtPhieuxuatkho>();

    [ForeignKey("LoaihangId")]
    [InverseProperty("Hanghoas")]
    public virtual Loaihang? Loaihang { get; set; }
}
