using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QL_HaiSan_HoangNhi.Models
{
    public class HoaDonItem:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int HangHoaId { get; set; }

        public string? TenHang { get; set; }

        public string? Dvt { get; set; }
        public string? GhiChu { get; set; }

        private decimal _donGia;
        public decimal DonGia { get => _donGia; 
            set { _donGia = value;
                OnPropertyChanged(nameof(DonGia));
                OnPropertyChanged(nameof(ThanhTien));
            } }

        private decimal _soLuong;
        public decimal SoLuong { get => _soLuong;
            set { _soLuong = value;
                OnPropertyChanged(nameof(SoLuong));
                OnPropertyChanged(nameof(ThanhTien));
            } }

        public decimal ThanhTien
        {
            get =>SoLuong * DonGia;
           
        }
    }
}

