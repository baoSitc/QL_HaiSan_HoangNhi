using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("SHIPPER")]
public partial class Shipper
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MASHIPPER")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Mashipper { get; set; }

    [Column("TENSHIPPER")]
    [StringLength(300)]
    public string? Tenshipper { get; set; }

    [Column("SDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [Column("DIACHI")]
    [StringLength(500)]
    public string? Diachi { get; set; }

    [Column("BIENSO")]
    [StringLength(50)]
    public string? Bienso { get; set; }

    [Column("CCCD")]
    [StringLength(50)]
    public string? Cccd { get; set; }

    [Column("TIENTAMGIU", TypeName = "decimal(18, 2)")]
    public decimal? Tientamgiu { get; set; }

    [Column("DANGHOATDONG")]
    public bool? Danghoatdong { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [InverseProperty("Shipper")]
    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    [InverseProperty("Shipper")]
    public virtual ICollection<ShipperNoptien> ShipperNoptiens { get; set; } = new List<ShipperNoptien>();
}
