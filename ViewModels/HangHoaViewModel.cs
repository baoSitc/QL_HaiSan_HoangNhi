using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QL_HaiSan_HoangNhi.Helpers;
using QL_HaiSan_HoangNhi.Models;
using QL_HaiSan_HoangNhi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public partial class HangHoaViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [ObservableProperty]
        private ObservableCollection<Hanghoa> danhSachHangHoa;
        [ObservableProperty]
        private Hanghoa hangHoaMoi = new();
        [ObservableProperty]
        private ObservableCollection<Loaihang> danhSachLoaiHang;
        public List<string> DanhSachDVT
        {
            get => DanhMucHelper.DanhSachDVT.OrderBy(x => x).ToList();
        }
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
                OnPropertyChanged(nameof(DanhSachHangHoa));
                return;
            }

            var data = App.Db.Hanghoas
                .Where(x =>
                    x.Tenhh.Contains(TuKhoa))
                .ToList();

            DanhSachHangHoa =
                new ObservableCollection<Hanghoa>(data);
            OnPropertyChanged(nameof(DanhSachHangHoa));
        }


        private Hanghoa _selectedHangHoa;
        public Models.Hanghoa SelectedHangHoa
        {
            get { return _selectedHangHoa; }
            set
            {
                _selectedHangHoa = value;
                OnPropertyChanged(nameof(SelectedHangHoa));
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HangHoaViewModel()
        {
            LoadLoaiHang();
            LoadData();
        }
        public void LoadLoaiHang()
        {
            DanhSachLoaiHang =
                new ObservableCollection<Loaihang>
            (
                App.Db.Loaihangs.ToList()
            );
        }
        [RelayCommand]
        public void LoadData()
        {
            var data = App.Db.Hanghoas.ToList();

            DanhSachHangHoa =
                new ObservableCollection<Hanghoa>(data);

            SelectedHangHoa = new Hanghoa();
        }

        [RelayCommand]
        public void Them()
        {
            var count = App.Db.Hanghoas.Count() + 1;

            SelectedHangHoa.Mahh = $"HH{count:0000}";
            SelectedHangHoa.Id = 0;
            App.Db.Hanghoas.Add(SelectedHangHoa);

            App.Db.SaveChanges();

            LoadData();
            OnPropertyChanged(nameof(DanhSachHangHoa));

            SelectedHangHoa = new Hanghoa();
        }
        [RelayCommand]
        public void MoFormLoaiHang()
        {
            var win = new LoaiHangWindow();

            win.ShowDialog();

            LoadLoaiHang();
            OnPropertyChanged(nameof(DanhSachLoaiHang));
        }
        [RelayCommand]
        public void Sua()
        {
            if (SelectedHangHoa == null || SelectedHangHoa.Id == 0)
            {
                return;
            }
            var hh = App.Db.Hanghoas.Find(SelectedHangHoa.Id);
            if (hh != null)
            {
                hh.Tenhh = SelectedHangHoa.Tenhh;
                
                hh.Dvt = SelectedHangHoa.Dvt;
                hh.Gianhap = SelectedHangHoa.Gianhap;
                hh.Giaban = SelectedHangHoa.Giaban;
                hh.Tonkho = SelectedHangHoa.Tonkho;
                hh.Dinhmucton = SelectedHangHoa.Dinhmucton;
                hh.Dangkinhdoanh = SelectedHangHoa.Dangkinhdoanh;
                hh.Hinhanh = SelectedHangHoa.Hinhanh;
                hh.Ghichu = SelectedHangHoa.Ghichu;
                App.Db.SaveChanges();
                LoadData();
                OnPropertyChanged(nameof(DanhSachHangHoa));
                SelectedHangHoa = new Hanghoa();
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
