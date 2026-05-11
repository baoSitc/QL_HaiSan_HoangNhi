using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QL_HaiSan_HoangNhi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public partial class LoaiHangViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Loaihang> danhSachLoaiHang;

        [ObservableProperty]
        private Loaihang loaiHangMoi = new();

        [ObservableProperty]
        private Loaihang loaiHangDangChon;
        public LoaiHangViewModel()
        {
            LoadData();
        }

        [RelayCommand]
        public void LoadData()
        {
            DanhSachLoaiHang =
                new ObservableCollection<Loaihang>
            (
                App.Db.Loaihangs.ToList()
            );
        }
        [RelayCommand]
        public void Them()
        {
            App.Db.Loaihangs.Add(LoaiHangMoi);
            App.Db.SaveChanges();
            LoadData();
            LoaiHangMoi = new Loaihang();
        }
        [RelayCommand]
        public void Sua()
        {
            App.Db.SaveChanges();
            LoadData();
        }
        [RelayCommand]
        public void Xoa()
        {
            if (LoaiHangDangChon != null)
            {
                App.Db.Loaihangs.Remove(LoaiHangDangChon);
                App.Db.SaveChanges();
                LoadData();
            }
        }

    }
}
