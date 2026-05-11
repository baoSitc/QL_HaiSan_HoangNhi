using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("LOAIHANG")]
public partial class Loaihang
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MALOAI")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Maloai { get; set; }

    [Column("TENLOAI")]
    [StringLength(200)]
    public string? Tenloai { get; set; }

    [Column("GHICHU")]
    [StringLength(500)]
    public string? Ghichu { get; set; }

    [InverseProperty("Loaihang")]
    public virtual ICollection<Hanghoa> Hanghoas { get; set; } = new List<Hanghoa>();
}
