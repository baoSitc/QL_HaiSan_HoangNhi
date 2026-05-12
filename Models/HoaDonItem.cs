using System;
using System.Collections.Generic;
using System.Text;

namespace QL_HaiSan_HoangNhi.Models
{
    public class HoaDonItem
    {
        public int HangHoaId { get; set; }

        public string TenHang { get; set; }

        public string Dvt { get; set; }
        public string GhiChu { get; set; }

        public decimal DonGia { get; set; }

        public decimal SoLuong { get; set; }

        public decimal ThanhTien
        {
            get => SoLuong * DonGia;
        }
    }
}

