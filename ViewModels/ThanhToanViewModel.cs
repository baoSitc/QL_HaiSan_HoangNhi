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

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public class ThanhToanViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                EmptyThanhToan();
            }
        }
        public ObservableCollection<CtHoadon> ChiTietHoaDon
        {
            get;
            set;
        }
        public void LoadData()
        {
            var data = App.Db.Hoadons.Include("Khachhang")
                .Where( x =>
                    x.Trangthai == "DANGGIAO"
                    &&
                    (x.TrangThaiThanhToan
                        == "CHUATHANHTOAN" ||  x.TrangThaiThanhToan
                        == "CONGNO"))
                .OrderByDescending(x => x.Ngaylap)
                .ToList();

            DanhSachHoaDon =
                new ObservableCollection<Hoadon>(data);
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
            SelectedHoaDon.Trangthai = "DANGGIAO";

            //NẾU CÒN NỢ
            if (ConLai > 0)
            {
                SelectedHoaDon
                .TrangThaiThanhToan
                    = "CONGNO";
           
            }
            else
            {
                SelectedHoaDon
                .TrangThaiThanhToan
                    = "DATHANHTOAN";
              
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
        public void EmptyThanhToan()
        {
            HinhThucThanhToan = string.Empty;
            TienKhachDua = 0;
            ConLai = 0;
            PhiShip = 0;
            PhiGuiXe = 0;
            Shipper = string.Empty;
            OnPropertyChanged(nameof(DanhSachShipper));
            OnPropertyChanged(nameof(TienKhachDua));
            OnPropertyChanged(nameof(PhiShip));
            OnPropertyChanged(nameof(PhiGuiXe));
            TinhConLai();
        }
    }
}
