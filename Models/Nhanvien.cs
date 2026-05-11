using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("NHANVIEN")]
public partial class Nhanvien
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MANV")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Manv { get; set; }

    [Column("TENNV")]
    [StringLength(300)]
    public string? Tennv { get; set; }

    [Column("SDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [Column("DIACHI")]
    [StringLength(500)]
    public string? Diachi { get; set; }

    [Column("USERNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("PASSWORDHASH")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Passwordhash { get; set; }

    [Column("VAITRO")]
    [StringLength(100)]
    public string? Vaitro { get; set; }

    [Column("DANGHOATDONG")]
    public bool? Danghoatdong { get; set; }

    [InverseProperty("Nhanvien")]
    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    [InverseProperty("Nhanvien")]
    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();

    [InverseProperty("Nhanvien")]
    public virtual ICollection<Phieuthu> Phieuthus { get; set; } = new List<Phieuthu>();

    [InverseProperty("Nhanvien")]
    public virtual ICollection<Phieuxuatkho> Phieuxuatkhos { get; set; } = new List<Phieuxuatkho>();

    [InverseProperty("Nhanvien")]
    public virtual ICollection<ShipperNoptien> ShipperNoptiens { get; set; } = new List<ShipperNoptien>();
}
