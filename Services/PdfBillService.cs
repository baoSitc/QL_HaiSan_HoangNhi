using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using QL_HaiSan_HoangNhi.Models;
using QL_HaiSan_HoangNhi.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;



namespace QL_HaiSan_HoangNhi.Services
{
    public class PdfBillService
    {
        public void ExportPdf(HoaDonTamViewModel hoaDon)
        {
            // =============================
            // TẠO PDF
            // =============================
            PdfDocument document =
              new PdfDocument();

            document.Info.Title ="Hoa Don Hai San";
            PdfPage page =document.AddPage();
            // bill K80
            page.Width = XUnit.FromMillimeter(80);

            page.Height = XUnit.FromMillimeter(250);

            XGraphics gfx = XGraphics.FromPdfPage(page);
            // =============================
            // FONT
            // =============================
          XFont fontTitle =new XFont("Verdana", 14,XFontStyle.Bold);
            XFont fontHoaDon = new XFont("Verdana", 10, XFontStyle.Bold);

            XFont fontNormal =new XFont("Verdana", 10, XFontStyle.Regular);

        XFont fontBold =new XFont("Verdana", 10, XFontStyle.Bold);

            XFont fontTenHang = new XFont("Verdana", 8, XFontStyle.Regular);
            int y = 20;
            // =============================
            // SHOP
            // =============================
            gfx.DrawString("HẢI SẢN HOÀNG NHI",fontTitle,XBrushes.Black,new XRect(0, y, page.Width, 20),
                XStringFormats.TopCenter); 
            y += 25;

            gfx.DrawString("HÓA ĐƠN BÁN HÀNG", fontHoaDon, XBrushes.Black, new XRect(0, y, page.Width, 20),
               XStringFormats.TopCenter);
            y += 15;
            gfx.DrawString("Số HĐ: " + hoaDon.SoHoaDon, fontNormal, XBrushes.Black, new XRect(0, y, page.Width, 20),
               XStringFormats.TopCenter);

            y += 15;
            gfx.DrawString("Ngày giờ:"+ DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontNormal,XBrushes.Black, new XRect(10, y, page.Width, 20),
                XStringFormats.TopCenter);

            y += 25;
            // =============================
            // KHÁCH HÀNG
            // =============================
            gfx.DrawString("KH: " + hoaDon.TenKhachHang,fontNormal, XBrushes.Black,10,y);

            y += 15;

            gfx.DrawString("SDT: " + hoaDon.SoDienThoai,fontNormal,XBrushes.Black,10, y);

            y += 5;
            XTextFormatter tf =
      new XTextFormatter(gfx);

            XRect rectDiaChi =
                new XRect(10, y, 180, 40);

            tf.DrawString(
                "DC: " + hoaDon.DiaChiGiao,
                fontNormal,
                XBrushes.Black,
                rectDiaChi,
                XStringFormats.TopLeft);
            if (hoaDon.DiaChiGiao!= null && hoaDon.DiaChiGiao.Length > 23)
                y += 40;
            else y += 25;
            // =============================
            // HEADER
            // =============================

            gfx.DrawString("Đơn Giá", fontBold, XBrushes.Black,10,y);

            gfx.DrawString("SL", fontBold,XBrushes.Black,120,y);

            gfx.DrawString( "TT", fontBold,XBrushes.Black,180, y);

            y += 15;

            gfx.DrawLine( XPens.Black, 10, y, 220,   y);

            y += 15;
            // =============================
            // DANH SÁCH HÀNG
            // =============================
            XPen dottedPen = new XPen(XColors.Brown, 1);

            dottedPen.DashStyle = XDashStyle.Dot;

            foreach (var item in hoaDon.GioHang)
            {
                gfx.DrawString( item.TenHang, fontNormal, XBrushes.Black,   10, y);
                y += 15;

                gfx.DrawString(item.DonGia.ToString("N0"), fontNormal, XBrushes.Black, 10, y);

                gfx.DrawString(  item.SoLuong.ToString("N2"), fontNormal,  XBrushes.Black, 120, y);

                //gfx.DrawString(    item.ThanhTien.ToString("N0"),  fontNormal,   XBrushes.Black,   160,  y );
                gfx.DrawString(item.ThanhTien.ToString("N0"),fontNormal,XBrushes.Black,
                                new XRect(150, y-10, 60, 20),
                                XStringFormats.TopRight);
                y += 5;
                gfx.DrawLine(dottedPen, 10, y, 220, y);
                y += 20;
            }

            y += 10;

            gfx.DrawLine(  XPens.Black,  10,    y,  220,  y);

            y += 20;
            // =============================
            // TỔNG TIỀN
            // =============================

            gfx.DrawString( "TỔNG:", fontBold, XBrushes.Black,   90,  y);

            gfx.DrawString(  hoaDon.TongTien.ToString("N0") + " đ", fontBold,  XBrushes.Black,  150,  y);

            y += 40;

            // =============================
            // FOOTER
            // =============================

            gfx.DrawString(   "Cảm ơn quý khách!",   fontNormal, XBrushes.Black, new XRect(0, y, page.Width, 20),
                XStringFormats.TopCenter);

            // =============================
            // SAVE FILE
            // =============================

            string folder =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Desktop),
                    "Bills");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName =
                $"HD_{hoaDon.HoaDonId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            string fullPath =
                Path.Combine(folder, fileName);

            document.Save(fullPath);

            // mở pdf
            Process.Start(new ProcessStartInfo()
            {
                FileName = fullPath,
                UseShellExecute = true
            });

        }
    }

}