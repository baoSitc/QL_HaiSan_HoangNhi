using CommunityToolkit.Mvvm.Input;
using QL_HaiSan_HoangNhi.Helpers;
using QL_HaiSan_HoangNhi.Models;
using QL_HaiSan_HoangNhi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public class HoaDonTamViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<HoaDonTamViewModel>? CloseTabAction;
        //In hóa đơn
        public ICommand InBillCommand
        {
            get;
            set;
        }

        public int HoaDonId
        {
            get;
            set;
        }
        public string SoHoaDon
        {
            get;
            set;
        }

        public string TenTab { get; set; }
        public ObservableCollection<HoaDonItem> GioHang
        {
            get;
            set;
        }
   = new ObservableCollection<HoaDonItem>();
        private decimal _tongTien;
        public decimal TongTien
        {
            get => _tongTien;
            set
            {
                _tongTien = value;

                OnPropertyChanged();
            }
        }
       
        public HoaDonTamViewModel()
        {
            TenTab = "Hóa đơn mới";
            InBillCommand =
  new RelayCommand(InBill);
        }
        public void InBill()
        {
            if (HoaDonId == 0)
                return;

            // chưa có hàng
            if (GioHang.Count == 0)
            {
                MessageBox.Show("Chưa có hàng hóa");

                return;
            }

            // tìm hóa đơn DB
            var hd = App.Db.Hoadons
                .FirstOrDefault(x =>
                    x.Id == HoaDonId);

            if (hd == null)
                return;

            // ==========================
            // UPDATE TRẠNG THÁI
            // ==========================
            SoHoaDon =hd.Sohd= TaoSoHoaDon();
            
            hd.Trangthai = "DANGGIAO";

            hd.TrangThaiThanhToan =
                "CHUATHANHTOAN";

            hd.Tongtien =
                TongTien;

            App.Db.SaveChanges();

            // ==========================
            // TRỪ TỒN KHO
            // ==========================

            //TruTonKho();

            // ==========================
            // IN BILL
            // ==========================

            PrintBill();

            // ==========================
            // ĐÓNG TAB
            // ==========================
            // xóa tab hiện tại


            CloseTabAction?.Invoke(this);
            EventBus.HoaDonDaIn?.Invoke();

            // tạo tab mới
            //   BanHangViewModel.TaoHoaDon();
        }
        public void PrintBill()
        {
            MessageBox.Show(
                "Đang in bill...");
            //     BillPrinter printer =
            //new BillPrinter();

            //     printer.HoaDon = this;

            //     printer.Print();

            PdfBillService pdf =
       new PdfBillService();

            pdf.ExportPdf(this);

        }
        //Tạo số hóa đơn
        public string TaoSoHoaDon()
        {
            string ngay =
    DateTime.Now.ToString("yyyyMMdd");

            var sohdCuoi = App.Db.Hoadons
                .Where(x =>
                    x.Sohd.StartsWith(ngay))
                .OrderByDescending(x => x.Sohd)
                .Select(x => x.Sohd)
                .FirstOrDefault();

            int stt = 1;

            if (sohdCuoi != null)
            {
                string soCuoi =
                    sohdCuoi.Substring(9);

                stt = int.Parse(soCuoi) + 1;
            }

            return $"{ngay}-{stt:0000}";
        }
        //khách hàng
        private ObservableCollection<Khachhang> _danhSachKhachHang;
        public ObservableCollection<Khachhang> DanhSachKhachHang
        {
            get => _danhSachKhachHang;
            set
            {
                _danhSachKhachHang = value;
                OnPropertyChanged();
            }
        }
        public void LoadKhachHang()
        {
            DanhSachKhachHang =
                new ObservableCollection<Khachhang>
            (
                App.Db.Khachhangs.ToList()
            );
        }
        private string _tenKhachHang;
        public string TenKhachHang
        {
            get => _tenKhachHang;
            set
            {
                _tenKhachHang = value;
                OnPropertyChanged();
            }
        }
        private string _diaChiGiao;
        public string DiaChiGiao
        {
            get => _diaChiGiao;
            set
            {
                _diaChiGiao = value;
                OnPropertyChanged();
            }
        }
        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set
            {
                _ghiChu = value;
                OnPropertyChanged();
            }
        }
        private string _soDienThoai;
        public string SoDienThoai
        {
            get => _soDienThoai;
            set
            {
                _soDienThoai = value;               
                OnPropertyChanged();

                TimKhachHang();
            }
        }
        //kiem tra khách hàng
        public Khachhang KiemTraKhachHang()
        {
            // không nhập sdt
            if (string.IsNullOrWhiteSpace(SoDienThoai) || string.IsNullOrWhiteSpace(TenKhachHang) || string.IsNullOrWhiteSpace(DiaChiGiao))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            // tìm khách
            var kh = App.Db.Khachhangs
                .FirstOrDefault(x => x.Sdt == SoDienThoai);

            // đã có
            if (kh != null)
                return kh;

            // chưa có -> tạo mới
            var count = App.Db.Khachhangs.Count() + 1;
            Khachhang newKh = new Khachhang()
            {             

                Makh = $"KH{count:0000}",
                Id = 0,
                Sdt = SoDienThoai,
                Tenkh = TenKhachHang,
                Diachi = DiaChiGiao
            };

            App.Db.Khachhangs.Add(newKh);

            App.Db.SaveChanges();
            LoadKhachHang();

            return newKh;
        }
        public void TimKhachHang()
        {
            if (string.IsNullOrWhiteSpace(SoDienThoai))
                return;

            var kh = App.Db.Khachhangs
                .FirstOrDefault(x => x.Sdt == SoDienThoai);

            if (kh != null)
            {
                TenKhachHang = kh.Tenkh;

                DiaChiGiao = kh.Diachi;
            }
        }
       

    }
}
