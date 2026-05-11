using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

[Table("CT_HOADON")]
[Index("HoadonId", Name = "IX_CT_HOADON_HOADON")]
public partial class CtHoadon
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("HOADON_ID")]
    public int? HoadonId { get; set; }

    [Column("HANGHOA_ID")]
    public int? HanghoaId { get; set; }

    [Column("SOLUONG", TypeName = "decimal(18, 2)")]
    public decimal? Soluong { get; set; }

    [Column("DONGIA", TypeName = "decimal(18, 2)")]
    public decimal? Dongia { get; set; }

    [Column("THANHTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Thanhtien { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [ForeignKey("HanghoaId")]
    [InverseProperty("CtHoadons")]
    public virtual Hanghoa? Hanghoa { get; set; }

    [ForeignKey("HoadonId")]
    [InverseProperty("CtHoadons")]
    public virtual Hoadon? Hoadon { get; set; }
}
