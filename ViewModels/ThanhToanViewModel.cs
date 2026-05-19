using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using QL_HaiSan_HoangNhi.Helpers;
using QL_HaiSan_HoangNhi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public class ThanhToanViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _tuKhoa;

        public string TuKhoa
        {
            get => _tuKhoa;

            set
            {
                _tuKhoa = value;

                OnPropertyChanged();

                LoadData();
            }
        }

        public List<string> DanhSachThanhToan
        {
            get;
        } = new()
            {
                "TM",
                "CK",
                "NỢ"
            };

        private string _hinhThucThanhToan;

        public string HinhThucThanhToan
        {
            get => _hinhThucThanhToan;

            set
            {
                _hinhThucThanhToan = value;

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

        private decimal _tienKhachDua;

        public decimal TienKhachDua
        {
            get => _tienKhachDua;

            set
            {
                _tienKhachDua = value;

                OnPropertyChanged();

                TinhConLai();
            }
        }
        public decimal SumDaThanhToan
        {
            get;
            set;
        }

        public decimal SumConLai
        {
            get;
            set;
        }

        private decimal _conLai;

        public decimal ConLai
        {
            get => _conLai;

            set
            {
                _conLai = value;

                OnPropertyChanged();
            }
        }
        public void TinhConLai()
        {
            if (SelectedHoaDon == null)
                return;

            decimal tongTien =
                SelectedHoaDon.Tongtien ?? 0;



            ConLai = tongTien - TienKhachDua;
                
        }
        public decimal PhiShip
        {
            get;
            set;
        }

        public decimal PhiGuiXe
        {
            get;
            set ;
        }

        public string Shipper
        {
            get;
            set;
        }
        private decimal _tongTienTatCa;

        public decimal TongTienTatCa
        {
            get => _tongTienTatCa;

            set
            {
                _tongTienTatCa = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<Hoadon> DanhSachHoaDon
        {
            get;
            set;
        }
        //Tạo ComboBox chọn Shipper
        private Shipper _selectedShipper;
        public Shipper SelectedShipper
        {
            get => _selectedShipper;
            set
            {
                _selectedShipper = value;
                OnPropertyChanged();
            }
        }

        private Hoadon _selectedHoaDon;

        public Hoadon SelectedHoaDon
        {
            get => _selectedHoaDon;

            set
            {
                _selectedHoaDon = value;

                OnPropertyChanged();

                LoadChiTietHoaDon();
                LoadThongTinThanhToan();
            }
        }
        public ObservableCollection<CtHoadon> ChiTietHoaDon
        {
            get;
            set;
        }
        public void LoadData()
        {
            var query = App.Db.Hoadons
                .Include("Khachhang")
                .Where(x =>
                    x.Trangthai == "DANGGIAO"
                    &&
                    (
                        x.TrangThaiThanhToan == "CHUATHANHTOAN"
                        ||
                        x.TrangThaiThanhToan == "CONNO"
                    ));

            // =====================================
            // FILTER
            // =====================================

            if (!string.IsNullOrWhiteSpace(TuKhoa))
            {
                query = query.Where(x =>

                    x.Khachhang.Tenkh.Contains(TuKhoa)

                    ||

                    x.Khachhang.Sdt.Contains(TuKhoa)

                    ||

                    x.Sohd.Contains(TuKhoa)
                );
            }

            var data = query
                .OrderByDescending(x => x.Ngaylap)
                .ToList();
            TongTienTatCa =
    data.Sum(x => x.Tongtien ?? 0);
            DanhSachHoaDon =
                new ObservableCollection<Hoadon>(data);
            SumDaThanhToan =
    data.Sum(x => x.Thanhtoan ?? 0);

            SumConLai =
                data.Sum(x => x.Conlai ?? 0);
            OnPropertyChanged(nameof(SumDaThanhToan));
            OnPropertyChanged(nameof(SumConLai));

            OnPropertyChanged(nameof(DanhSachHoaDon));
        }
        public void LoadChiTietHoaDon()
        {
            if (SelectedHoaDon == null)
                return;

            var data = App.Db.CtHoadons.Include<CtHoadon, Hanghoa>(x => x.Hanghoa)
                .Where(x =>
                    x.HoadonId == SelectedHoaDon.Id)
                .ToList();

            ChiTietHoaDon =
                new ObservableCollection<CtHoadon>(data);

            OnPropertyChanged(nameof(ChiTietHoaDon));
        }
        public ICommand ThanhToanCommand
        {
            get;
            set;
        }
        //Constructor
        public ThanhToanViewModel()
        {
            LoadData();
            ThanhToanCommand =
                new RelayCommand(ThanhToan);
            EventBus.HoaDonDaIn += LoadData;
            LoadShipper();

        }
        public void ThanhToan()
        {
            if (SelectedHoaDon == null)
                return;
            SelectedHoaDon.Thanhtoan = TienKhachDua;
            SelectedHoaDon.Phiship = PhiShip;
            SelectedHoaDon.Phiguixe = PhiGuiXe;
            SelectedHoaDon.ShipperId = SelectedShipper?.Id;
            SelectedHoaDon.HinhthucThanhtoan = HinhThucThanhToan;
            SelectedHoaDon.Conlai = ConLai;
            SelectedHoaDon.Ghichu = GhiChu;


            //NẾU CÒN NỢ
            if (ConLai > 0)
            {
                SelectedHoaDon
                .TrangThaiThanhToan
                    = "CONNO";
                SelectedHoaDon.Trangthai = "HOANTHANH";
                SelectedHoaDon.DaNopTien = false;

            }
            else
            {
                SelectedHoaDon
                .TrangThaiThanhToan
                    = "DATHANHTOAN";
                SelectedHoaDon.Trangthai = "HOANTHANH";
                SelectedHoaDon.DaNopTien = true;

            }



            App.Db.SaveChanges();

            LoadData();

            MessageBox.Show(
                "Đã thanh toán");
        }
        //load danh sách shipper
        public ObservableCollection<Shipper> DanhSachShipper
        {
            get;
            set;
        }
        public void LoadShipper()
        {
            var data = App.Db.Shippers.OrderBy(s => s.Tenshipper).ToList();
            DanhSachShipper =
                new ObservableCollection<Shipper>(data);
            OnPropertyChanged(nameof(DanhSachShipper));
        }
        //Xóa trắng thông tin thanh toán
        public void LoadThongTinThanhToan()
        {
            if (SelectedHoaDon == null)
                return;

            HinhThucThanhToan =
                SelectedHoaDon.HinhthucThanhtoan ?? "TM";

            TienKhachDua =
                SelectedHoaDon.Thanhtoan ?? 0;

            PhiShip =
                SelectedHoaDon.Phiship ?? 0;

            PhiGuiXe =
                SelectedHoaDon.Phiguixe ?? 0;
            GhiChu = SelectedHoaDon.Ghichu ?? "";

            SelectedShipper =
                DanhSachShipper.FirstOrDefault(x =>
                    x.Id == SelectedHoaDon.ShipperId);

            TinhConLai();
        }
    }
}
