using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
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
    public class ThongKeShipperViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime TuNgay
        {
            get;
            set;
        } = DateTime.Today.AddDays(-7);

        public DateTime DenNgay
        {
            get;
            set;
        } = DateTime.Today;

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
        public ObservableCollection<Shipper> DanhSachShipper
        {
            get;
            set;
        }
        public void LoadShipper()
        {
            var data = App.Db.Shippers
                .OrderBy(s => s.Tenshipper)
                .ToList();

            // =================================
            // THÊM ITEM TẤT CẢ
            // =================================

            data.Insert(0, new Shipper()
            {
                Id = 0,
                Tenshipper = "TẤT CẢ"
            });

            DanhSachShipper =
                new ObservableCollection<Shipper>(data);

            OnPropertyChanged(nameof(DanhSachShipper));

            // mặc định chọn tất cả

            SelectedShipper =
                DanhSachShipper.FirstOrDefault();
        }
        public ObservableCollection<Hoadon> DanhSachHoaDon
        {
            get;
            set;
        }
        public decimal TongPhaiNop
        {
            get;
            set;
        }

        public decimal TongDaNop
        {
            get;
            set;
        }

        public decimal TongChuaNop
        {
            get;
            set;
        }
        public decimal TongTienShipper
        {
            get;
            set;
        }
        public decimal TongTienGuiXe
        {
            get;
            set;
        }
        public ICommand NopTienCommand
        {
            get;
            set;
        }
        private Hoadon _selectedHoaDon;

        public Hoadon SelectedHoaDon
        {
            get => _selectedHoaDon;

            set
            {
                _selectedHoaDon = value;

                OnPropertyChanged();
            }
        }
        public void LoadData()
        {
            var query = App.Db.Hoadons
                .Include("Shipper")
                .Include("Khachhang")
                .Where(x =>

                    x.ShipperId != null

                    &&

                    x.HinhthucThanhtoan != "NỢ"

                    &&

                    x.Ngaylap >= TuNgay

                    &&

                    x.Ngaylap <= DenNgay
                    &&
                    (x.Trangthai == "DANGGIAO" || x.Trangthai == "HOANTHANH")
                );

            if (SelectedShipper != null && SelectedShipper.Id > 0)
            {
                query = query.Where(x =>
                    x.ShipperId == SelectedShipper.Id);
            }

            var data = query.ToList();

            DanhSachHoaDon =
                new ObservableCollection<Hoadon>(data);

            // KPI

            TongPhaiNop =
                data.Sum(x => x.Conlai ?? 0);

            TongDaNop =
                data
                .Where(x => x.DaNopTien == true)
                .Sum(x => x.Conlai ?? 0);

            TongChuaNop =
                data
                .Where(x => x.DaNopTien != true)
                .Sum(x => x.Conlai ?? 0);

            TongTienShipper =
                data.Sum(x => x.Phiship ?? 0);

            TongTienGuiXe =
                data.Sum(x => x.Phiguixe ?? 0);

            OnPropertyChanged(nameof(TongPhaiNop));
            OnPropertyChanged(nameof(TongDaNop));
            OnPropertyChanged(nameof(TongChuaNop));
            OnPropertyChanged(nameof(TongTienShipper));
            OnPropertyChanged(nameof(TongTienGuiXe));
            OnPropertyChanged(nameof(DanhSachHoaDon));
            OnPropertyChanged(nameof(DaNopTien));
        }
        public bool? DaNopTien
        {
            get;
            set;
        }
        public ICommand FillterCommand
        {
            get;
            set;
        }


        //Constructor
        public ThongKeShipperViewModel()
        {
            LoadShipper();
            LoadData();
            NopTienCommand =
                new RelayCommand(NopTien);
            FillterCommand =
                new RelayCommand(() => LoadData());
        }

        
        public void NopTien()
        {
            if (SelectedHoaDon == null)
                return;
            var confirm = MessageBox.Show(
                $"Xác nhận shipper đã nộp tiền bill {SelectedHoaDon.Sohd} ?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes)
                return;
            SelectedHoaDon.Thanhtoan = SelectedHoaDon.Tongtien;
            SelectedHoaDon.Conlai = 0;
            SelectedHoaDon.DaNopTien = true;
            SelectedHoaDon.TrangThaiThanhToan = "DATHANHTOAN";
            SelectedHoaDon.Trangthai = "HOANTHANH";
            SelectedHoaDon.NgayNopTien = DateTime.Now;
            App.Db.SaveChanges();
            LoadData();
            OnPropertyChanged(nameof(TongPhaiNop));
            OnPropertyChanged(nameof(TongDaNop));
            OnPropertyChanged(nameof(TongChuaNop));
            OnPropertyChanged(nameof(TongTienShipper));
            OnPropertyChanged(nameof(TongTienGuiXe));
            OnPropertyChanged(nameof(DanhSachHoaDon));
            OnPropertyChanged(nameof(DaNopTien));

        }
    }
}
