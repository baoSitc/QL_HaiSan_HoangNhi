using QL_HaiSan_HoangNhi.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace QL_HaiSan_HoangNhi.Services
{
    public class BillPrinter
    {
        public HoaDonTamViewModel HoaDon
        {
            get;
            set;
        }

        public void Print()
        {
            PrintDocument pd =
                new PrintDocument();

            // bill K80
            pd.DefaultPageSettings.PaperSize =
                new PaperSize("K80", 280, 1000);

            pd.PrintPage += Pd_PrintPage;

            //in ra file pdf thay vì in ra máy in
            
            pd.Print();
        }

        private void Pd_PrintPage(
            object sender,
            PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font font = new Font("Arial", 10);

            Font fontBold =
                new Font("Arial", 11, FontStyle.Bold);

            int y = 10;

            // =========================
            // SHOP
            // =========================

            g.DrawString(
                "HẢI SẢN HOÀNG NHI",
                fontBold,
                Brushes.Black,
                50,
                y);

            y += 30;

            g.DrawString(
                DateTime.Now.ToString(
                    "dd/MM/yyyy HH:mm"),
                font,
                Brushes.Black,
                10,
                y);

            y += 25;

            // =========================
            // KHÁCH
            // =========================

            g.DrawString(
                "KH: " + HoaDon.TenKhachHang,
                font,
                Brushes.Black,
                10,
                y);

            y += 20;

            g.DrawString(
                "SDT: " + HoaDon.SoDienThoai,
                font,
                Brushes.Black,
                10,
                y);

            y += 20;

            g.DrawString(
                "ĐC: " + HoaDon.DiaChiGiao,
                font,
                Brushes.Black,
                10,
                y);

            y += 30;

            // =========================
            // HEADER
            // =========================

            g.DrawString(
                "Tên",
                fontBold,
                Brushes.Black,
                10,
                y);

            g.DrawString(
                "SL",
                fontBold,
                Brushes.Black,
                140,
                y);

            g.DrawString(
                "TT",
                fontBold,
                Brushes.Black,
                190,
                y);

            y += 20;

            // =========================
            // HÀNG HÓA
            // =========================

            foreach (var item in HoaDon.GioHang)
            {
                g.DrawString(
                    item.TenHang,
                    font,
                    Brushes.Black,
                    10,
                    y);

                g.DrawString(
                    item.SoLuong.ToString(),
                    font,
                    Brushes.Black,
                    140,
                    y);

                g.DrawString(
                    item.ThanhTien
                        .ToString("N0"),
                    font,
                    Brushes.Black,
                    190,
                    y);

                y += 20;
            }

            y += 20;

            // =========================
            // TỔNG TIỀN
            // =========================

            g.DrawString(
                "TỔNG:",
                fontBold,
                Brushes.Black,
                100,
                y);

            g.DrawString(
                HoaDon.TongTien
                    .ToString("N0") + " đ",
                fontBold,
                Brushes.Black,
                170,
                y);

            y += 40;

            // =========================
            // FOOTER
            // =========================

            g.DrawString(
                "Cảm ơn quý khách!",
                font,
                Brushes.Black,
                60,
                y);
        }
    }
}
