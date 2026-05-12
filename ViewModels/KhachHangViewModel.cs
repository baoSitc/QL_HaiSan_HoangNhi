using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QL_HaiSan_HoangNhi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace QL_HaiSan_HoangNhi.ViewModels
{

    public partial class KhachHangViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [ObservableProperty]
        private ObservableCollection<Khachhang> danhSachKhachHang;

        private string _tuKhoa;
        public string TuKhoa
        {
            get => _tuKhoa;
            set
            {
                _tuKhoa = value;
                OnPropertyChanged(nameof(TuKhoa));
                TimKiem();
            }
        }
        public void TimKiem()
        {
            if (string.IsNullOrWhiteSpace(TuKhoa))
            {
                LoadData();
                OnPropertyChanged(nameof(DanhSachKhachHang));
                return;
            }

            var data = App.Db.Khachhangs
                .Where(x =>
                    x.Sdt.Contains(TuKhoa)
        || x.Tenkh.Contains(TuKhoa)
        || x.Zalo.Contains(TuKhoa))

                .ToList();

            DanhSachKhachHang =
                new ObservableCollection<Khachhang>(data);
            OnPropertyChanged(nameof(DanhSachKhachHang));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Khachhang _selectedKhachHang;
        public Models.Khachhang SelectedKhachHang
        {
            get { return _selectedKhachHang; }
            set
            {
                _selectedKhachHang = value;
                OnPropertyChanged(nameof(SelectedKhachHang));
            }

        }

        public void LoadData()
        {
            var data = App.Db.Khachhangs.ToList();
            DanhSachKhachHang = new ObservableCollection<Khachhang>(data);
            OnPropertyChanged(nameof(DanhSachKhachHang));
        }
        public KhachHangViewModel()
        {
            LoadData();
        }
        [RelayCommand]
        public void AddKhachHang()
        {
            //Kiểm tra nếu có khách hàng nào đang được chọn thì không thêm mới
            //kiểm tra trùng so điện thoại
            var existingKhachHang = App.Db.Khachhangs.FirstOrDefault(x => x.Sdt == SelectedKhachHang.Sdt);
            if (existingKhachHang != null)
            {
                MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số điện thoại khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedKhachHang != null)
            {
                var count = App.Db.Khachhangs.Count() + 1;

                SelectedKhachHang.Makh = $"KH{count:0000}";
                SelectedKhachHang.Id = 0;
                App.Db.Khachhangs.Add(SelectedKhachHang);
                App.Db.SaveChanges();
                LoadData();
            }
        }

        [RelayCommand]
        public void UpdateKhachHang()
        {
            var existingKhachHang = App.Db.Khachhangs.Find(SelectedKhachHang.Id);
            if (existingKhachHang != null)
            {
                existingKhachHang.Tenkh = SelectedKhachHang.Tenkh;
                existingKhachHang.Sdt = SelectedKhachHang.Sdt;
                existingKhachHang.Diachi = SelectedKhachHang.Diachi;
                existingKhachHang.Zalo = SelectedKhachHang.Zalo;
                App.Db.SaveChanges();
                LoadData();
            }

        }
        [RelayCommand]
        public void DeleteKhachHang(Khachhang khachHang)
        {
            var existingKhachHang = App.Db.Khachhangs.Find(SelectedKhachHang.Id);
            if (existingKhachHang != null)
            {
                App.Db.Khachhangs.Remove(existingKhachHang);
                App.Db.SaveChanges();
                LoadData();
            }


        }
    }
}
