using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("SHIPPER_NOPTIEN")]
public partial class ShipperNoptien
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("SHIPPER_ID")]
    public int? ShipperId { get; set; }

    [Column("NGAYNOP", TypeName = "datetime")]
    public DateTime? Ngaynop { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("SOTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Sotien { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [ForeignKey("NhanvienId")]
    [InverseProperty("ShipperNoptiens")]
    public virtual Nhanvien? Nhanvien { get; set; }

    [ForeignKey("ShipperId")]
    [InverseProperty("ShipperNoptiens")]
    public virtual Shipper? Shipper { get; set; }
}
