using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("NHACUNGCAP")]
public partial class Nhacungcap
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MANCC")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Mancc { get; set; }

    [Column("TENNCC")]
    [StringLength(300)]
    public string? Tenncc { get; set; }

    [Column("SDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    [Column("DIACHI")]
    [StringLength(500)]
    public string? Diachi { get; set; }

    [Column("EMAIL")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("GHICHU")]
    [StringLength(1000)]
    public string? Ghichu { get; set; }

    [InverseProperty("Nhacungcap")]
    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();
}
