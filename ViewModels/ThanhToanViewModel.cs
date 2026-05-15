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
    public class ThanhToanViewModel:BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Hoadon>DanhSachHoaDon
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

                LoadChiTietHoaDon();
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
                .Where(x =>
                    x.Trangthai == "DANGGIAO"
                    &&
                    x.TrangThaiThanhToan
                        == "CHUATHANHTOAN")
                .OrderByDescending(x => x.Ngaylap)
                .ToList();

            DanhSachHoaDon =
                new ObservableCollection<Hoadon>(data);
        }
        public void LoadChiTietHoaDon()
        {
            if (SelectedHoaDon == null)
                return;

            var data = App.Db.CtHoadons
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
           
        }
        public void ThanhToan()
        {
            if (SelectedHoaDon == null)
                return;

            SelectedHoaDon
                .TrangThaiThanhToan
                    = "DATHANHTOAN";

            App.Db.SaveChanges();

            LoadData();

            MessageBox.Show(
                "Đã thanh toán");
        }
    }
}
