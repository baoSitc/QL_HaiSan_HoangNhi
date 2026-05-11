using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QL_HaiSan_HoangNhi.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CtHoadon> CtHoadons { get; set; }

    public virtual DbSet<CtPhieuxuatkho> CtPhieuxuatkhos { get; set; }

    public virtual DbSet<Hanghoa> Hanghoas { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaihang> Loaihangs { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<NhatkyHethong> NhatkyHethongs { get; set; }

    public virtual DbSet<Phieunhap> Phieunhaps { get; set; }

    public virtual DbSet<Phieuthu> Phieuthus { get; set; }

    public virtual DbSet<Phieuxuatkho> Phieuxuatkhos { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<ShipperNoptien> ShipperNoptiens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.0.0.251\\sqlexpress;Database=QL_HAISAN_HOANGNHI;User Id=hoangnhi;Password=Hoangnhi@123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CtHoadon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_HOADO__3214EC273F466844");

            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hanghoa).WithMany(p => p.CtHoadons).HasConstraintName("FK__CT_HOADON__HANGH__4316F928");

            entity.HasOne(d => d.Hoadon).WithMany(p => p.CtHoadons).HasConstraintName("FK__CT_HOADON__HOADO__4222D4EF");
        });

        modelBuilder.Entity<CtPhieuxuatkho>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_PHIEU__3214EC275BE2A6F2");

            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hanghoa).WithMany(p => p.CtPhieuxuatkhos).HasConstraintName("FK__CT_PHIEUX__HANGH__5FB337D6");

            entity.HasOne(d => d.Phieuxuatkho).WithMany(p => p.CtPhieuxuatkhos).HasConstraintName("FK__CT_PHIEUX__PHIEU__5EBF139D");
        });

        modelBuilder.Entity<Hanghoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HANGHOA__3214EC2703317E3D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Dangkinhdoanh).HasDefaultValue(true);
            entity.Property(e => e.Dinhmucton).HasDefaultValue(0m);
            entity.Property(e => e.Giaban).HasDefaultValue(0m);
            entity.Property(e => e.Gianhap).HasDefaultValue(0m);
            entity.Property(e => e.Tonkho).HasDefaultValue(0m);

            entity.HasOne(d => d.Loaihang).WithMany(p => p.Hanghoas).HasConstraintName("FK__HANGHOA__LOAIHAN__0DAF0CB0");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HOADON__3214EC2731EC6D26");

            entity.Property(e => e.Conlai).HasDefaultValue(0m);
            entity.Property(e => e.Giamgia).HasDefaultValue(0m);
            entity.Property(e => e.Ngaylap).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Phiguixe).HasDefaultValue(0m);
            entity.Property(e => e.Phiship).HasDefaultValue(0m);
            entity.Property(e => e.Thanhtoan).HasDefaultValue(0m);
            entity.Property(e => e.Tongtien).HasDefaultValue(0m);

            entity.HasOne(d => d.Khachhang).WithMany(p => p.Hoadons).HasConstraintName("FK__HOADON__KHACHHAN__3A81B327");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Hoadons).HasConstraintName("FK__HOADON__NHANVIEN__3B75D760");

            entity.HasOne(d => d.Shipper).WithMany(p => p.Hoadons).HasConstraintName("FK__HOADON__SHIPPER___3C69FB99");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KHACHHAN__3214EC27145C0A3F");

            entity.Property(e => e.Congno).HasDefaultValue(0m);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Danghoatdong).HasDefaultValue(true);
            entity.Property(e => e.Hanmucno).HasDefaultValue(0m);
        });

        modelBuilder.Entity<Loaihang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOAIHANG__3214EC277F60ED59");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NHACUNGC__3214EC27108B795B");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NHANVIEN__3214EC271ED998B2");

            entity.Property(e => e.Danghoatdong).HasDefaultValue(true);
        });

        modelBuilder.Entity<NhatkyHethong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NHATKY_H__3214EC27628FA481");

            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Phieunhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PHIEUNHA__3214EC2729572725");

            entity.Property(e => e.Ngaynhap).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Tongtien).HasDefaultValue(0m);

            entity.HasOne(d => d.Nhacungcap).WithMany(p => p.Phieunhaps).HasConstraintName("FK__PHIEUNHAP__NHACU__2E1BDC42");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Phieunhaps).HasConstraintName("FK__PHIEUNHAP__NHANV__2F10007B");
        });

        modelBuilder.Entity<Phieuthu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PHIEUTHU__3214EC2745F365D3");

            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngaythu).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Khachhang).WithMany(p => p.Phieuthus).HasConstraintName("FK__PHIEUTHU__KHACHH__49C3F6B7");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Phieuthus).HasConstraintName("FK__PHIEUTHU__NHANVI__4AB81AF0");
        });

        modelBuilder.Entity<Phieuxuatkho>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PHIEUXUA__3214EC275535A963");

            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngayxuat).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Phieuxuatkhos).HasConstraintName("FK__PHIEUXUAT__NHANV__59063A47");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SHIPPER__3214EC27239E4DCF");

            entity.Property(e => e.Danghoatdong).HasDefaultValue(true);
            entity.Property(e => e.Tientamgiu).HasDefaultValue(0m);
        });

        modelBuilder.Entity<ShipperNoptien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SHIPPER___3214EC274D94879B");

            entity.Property(e => e.Ngaynop).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.ShipperNoptiens).HasConstraintName("FK__SHIPPER_N__NHANV__52593CB8");

            entity.HasOne(d => d.Shipper).WithMany(p => p.ShipperNoptiens).HasConstraintName("FK__SHIPPER_N__SHIPP__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
