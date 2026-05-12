using CommunityToolkit.Mvvm.Input;
using QL_HaiSan_HoangNhi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public partial class BanHangViewModel:BaseViewModel,INotifyPropertyChanged
    {
        public ObservableCollection<Hanghoa> DanhSachHangHoa
        {
            get;
            set;
        }
        private Hanghoa _selectedHangHoa;

        public Hanghoa SelectedHangHoa
        {
            get => _selectedHangHoa;
            set
            {
                _selectedHangHoa = value;

                OnPropertyChanged();
            }
        }
        private decimal _soLuong = 1;

        public decimal SoLuong
        {
            get => _soLuong;
            set
            {
                _soLuong = value;

                OnPropertyChanged();
            }
        }
        //giỏ hàng
        public ObservableCollection<HoaDonItem> GioHang
        {
            get;
            set;
        } = new ObservableCollection<HoaDonItem>();
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
        private Khachhang _selectedKhachHang;
        public Khachhang SelectedKhachHang
        {
            get => _selectedKhachHang;
            set
            {
                _selectedKhachHang = value;

                OnPropertyChanged();

                if (value != null)
                {
                    TenKhachHang = value.Tenkh;

                    DiaChiGiao = value.Diachi;
                }
                else { 
                    TenKhachHang = string.Empty;
                    DiaChiGiao = string.Empty;
                }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Contructor
        public BanHangViewModel()
        {
            LoadKhachHang();
            //load hàng hóa
            DanhSachHangHoa =
               new ObservableCollection<Hanghoa>
           (
               App.Db.Hanghoas.ToList()
           );
            ThemGioHangCommand = new RelayCommand(ThemGioHang);
            ChonHangCommand =   new RelayCommand<Hanghoa>(ChonHang);
        }
        //Command
        public ICommand ThemGioHangCommand
        {
            get;
            set;
        }
        public ICommand ChonHangCommand
        {
            get;
            set;
        }
        public void ThemGioHang()
        {
            if (SelectedHangHoa == null)
                return;

            HoaDonItem item = new HoaDonItem()
            {
                HangHoaId = SelectedHangHoa.Id,
                TenHang = SelectedHangHoa.Tenhh,
                Dvt = SelectedHangHoa.Dvt,
                DonGia = SelectedHangHoa.Giaban ?? 0,
                SoLuong = SoLuong
            };

            GioHang.Add(item);

            TinhTongTien();
        }
        //tính tổng tiền
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
        public void TinhTongTien()
        {
            TongTien = GioHang.Sum(x => x.ThanhTien);
        }
        public void ChonHang(Hanghoa hh)
        {
            var item = GioHang
                .FirstOrDefault(x => x.HangHoaId == hh.Id);

            if (item != null)
            {
                item.SoLuong++;

                TinhTongTien();

                return;
            }

            GioHang.Add(new HoaDonItem()
            {
                HangHoaId = hh.Id,
                TenHang = hh.Tenhh,
                DonGia = hh.Giaban ?? 0,
                SoLuong = 1,
                Dvt = hh.Dvt
            });

            TinhTongTien();
        }
    }
}
