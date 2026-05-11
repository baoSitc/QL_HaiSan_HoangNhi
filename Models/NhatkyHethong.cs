using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("NHATKY_HEThong")]
public partial class NhatkyHethong
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("NHANVIEN_ID")]
    public int? NhanvienId { get; set; }

    [Column("HANHDONG")]
    [StringLength(500)]
    public string? Hanhdong { get; set; }

    [Column("DULIEU")]
    public string? Dulieu { get; set; }
}
