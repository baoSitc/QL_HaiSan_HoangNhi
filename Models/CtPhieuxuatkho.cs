using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("CT_PHIEUXUATKHO")]
public partial class CtPhieuxuatkho
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("PHIEUXUATKHO_ID")]
    public int? PhieuxuatkhoId { get; set; }

    [Column("HANGHOA_ID")]
    public int? HanghoaId { get; set; }

    [Column("SOLUONG", TypeName = "decimal(18, 2)")]
    public decimal? Soluong { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [ForeignKey("HanghoaId")]
    [InverseProperty("CtPhieuxuatkhos")]
    public virtual Hanghoa? Hanghoa { get; set; }

    [ForeignKey("PhieuxuatkhoId")]
    [InverseProperty("CtPhieuxuatkhos")]
    public virtual Phieuxuatkho? Phieuxuatkho { get; set; }
}
