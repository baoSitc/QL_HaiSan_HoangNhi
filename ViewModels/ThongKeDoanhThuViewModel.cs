using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using QL_HaiSan_HoangNhi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Drawing;

namespace QL_HaiSan_HoangNhi.ViewModels
{
    public class ThongKeDoanhThuViewModel : BaseViewModel, INotifyPropertyChanged
    {
        // =========================
        // FILTER
        // =========================

        private DateTime? _tuNgay = DateTime.Today;

        public DateTime? TuNgay
        {
            get => _tuNgay;
            set
            {
                _tuNgay = value;
                OnPropertyChanged();
            }
        }
        private DateTime? _denNgay = DateTime.Today;

        public DateTime? DenNgay
        {
            get => _denNgay;
            set
            {
                _denNgay = value;
                OnPropertyChanged();
            }
        }
        // =========================
        // KPI
        // =========================

        private decimal _tongDoanhThu;

        public decimal TongDoanhThu
        {
            get => _tongDoanhThu;
            set
            {
                _tongDoanhThu = value;
                OnPropertyChanged();
            }
        }
        private decimal _tongTienMat;

        public decimal TongTienMat
        {
            get => _tongTienMat;
            set
            {
                _tongTienMat = value;
                OnPropertyChanged();
            }
        }
        private decimal _tongChuyenKhoan;

        public decimal TongChuyenKhoan
        {
            get => _tongChuyenKhoan;
            set
            {
                _tongChuyenKhoan = value;
                OnPropertyChanged();
            }
        }
        private decimal _tongCongNo;

        public decimal TongCongNo
        {
            get => _tongCongNo;
            set
            {
                _tongCongNo = value;
                OnPropertyChanged();
            }
        }
        private int _tongHoaDon;

        public int TongHoaDon
        {
            get => _tongHoaDon;
            set
            {
                _tongHoaDon = value;
                OnPropertyChanged();
            }
        }
        // =========================
        // COMMAND
        // =========================

        public RelayCommand LocCommand
        {
            get;
            set;
        }

        // =========================
        // CONSTRUCTOR
        // =========================

        public ThongKeDoanhThuViewModel()
        {
            LocCommand =
                new RelayCommand(LoadData);

            LoadData();
        }

        public void LoadData()
        {
            DateTime tuNgay =
                TuNgay?.Date ?? DateTime.Today;

            DateTime denNgay =
                (DenNgay?.Date ?? DateTime.Today)
                .AddDays(1)
                .AddSeconds(-1);

            var data = App.Db.Hoadons
                .Include("Khachhang")
                .Include("Shipper")
                .Where(x =>
                    x.Ngaylap >= tuNgay
                    &&
                    x.Ngaylap <= denNgay
                    &&
                    x.Trangthai == "HOANTHANH");

            // FILTER SEARCH

            if (!string.IsNullOrWhiteSpace(TuKhoa))
            {
                data = data.Where(x =>
                    x.Sohd.Contains(TuKhoa)
                    ||
                    x.Khachhang.Tenkh.Contains(TuKhoa)
                    ||
                    x.Khachhang.Sdt.Contains(TuKhoa)
                  ||
                   x.HinhthucThanhtoan.Contains(TuKhoa)
                  ||
                    x.Shipper.Tenshipper.Contains(TuKhoa));
            }

            var result = data
                .OrderByDescending(x => x.Ngaylap)
                .ToList();

            DanhSachHoaDon =
                new ObservableCollection<Hoadon>(result);

            // KPI

            TongDoanhThu =
                result.Sum(x => x.Tongtien ?? 0);

            TongTienMat =
                result.Where(x => x.HinhthucThanhtoan == "TM")
                    .Sum(x => x.Thanhtoan ?? 0);

            TongChuyenKhoan =
                result.Where(x => x.HinhthucThanhtoan == "CK")
                    .Sum(x => x.Thanhtoan ?? 0);

            TongCongNo =
                result.Where(x => x.TrangThaiThanhToan == "CONNO")
                    .Sum(x => x.Conlai ?? 0);

            TongHoaDon =
                result.Count;

            TongGrid =
                result.Sum(x => x.Tongtien ?? 0);

            // ======================
            // CHART DOANH THU
            // ======================

            var doanhThuTheoNgay = result
                .GroupBy(x => x.Ngaylap.Value.Date)
                .Select(g => new
                {
                    Ngay = g.Key,
                    Tong = g.Sum(x => x.Tongtien ?? 0)
                })
                .OrderBy(x => x.Ngay)
                .ToList();

            Labels = doanhThuTheoNgay
                .Select(x => x.Ngay.ToString("dd/MM"))
                .ToArray();

            XAxes = new Axis[]
{
    new Axis
    {
        Labels = Labels
    }
};
            SeriesDoanhThu = new ISeries[]
            {
    new ColumnSeries<double>
    {
        Values = doanhThuTheoNgay
            .Select(x => (double)x.Tong)
            .ToArray(),

        Name = "Doanh thu"
    }
            };

            // ======================
            // PIE TM / CK / NỢ
            // ======================

            decimal tienMat = result
                .Where(x => x.HinhthucThanhtoan == "TM")
                .Sum(x => x.Thanhtoan ?? 0);

            decimal chuyenKhoan = result
                .Where(x => x.HinhthucThanhtoan == "CK")
                .Sum(x => x.Thanhtoan ?? 0);

            decimal congNo = result
                .Where(x => x.TrangThaiThanhToan == "CONNO")
                .Sum(x => x.Conlai ?? 0);

            SeriesThanhToan = new ISeries[]
            {
    new PieSeries<double>
    {
        Values = new double[] { (double)tienMat },
        Name = "TM",  ToolTipLabelFormatter = point =>
        $"{point.Context.Series.Name} : " +
        $"{point.Coordinate.PrimaryValue:N0}"
    },

    new PieSeries<double>
    {
        Values = new double[] { (double)chuyenKhoan },
        Name = "CK",DataLabelsFormatter = point =>
            point.Coordinate.PrimaryValue.ToString("N0")
    },

    new PieSeries<double>
    {
        Values = new double[] { (double)congNo },
        Name = "NỢ",DataLabelsFormatter = point =>
            point.Coordinate.PrimaryValue.ToString("N0")
    }
            };
        }


        // =========================
        //GIAI ĐOẠN 2
        // =========================
        private ObservableCollection<Hoadon> _danhSachHoaDon;

        public ObservableCollection<Hoadon> DanhSachHoaDon
        {
            get => _danhSachHoaDon;

            set
            {
                _danhSachHoaDon = value;
                OnPropertyChanged();
            }
        }
        private string _tuKhoa;

        public string TuKhoa
        {
            get => _tuKhoa;

            set
            {
                _tuKhoa = value;
                OnPropertyChanged();
            }
        }
        private decimal _tongGrid;

        public decimal TongGrid
        {
            get => _tongGrid;

            set
            {
                _tongGrid = value;
                OnPropertyChanged();
            }
        }
        // =========================
        //GIAI ĐOẠN 3
        // =========================
        private ISeries[] _seriesDoanhThu;

        public ISeries[] SeriesDoanhThu
        {
            get => _seriesDoanhThu;

            set
            {
                _seriesDoanhThu = value;
                OnPropertyChanged();
            }
        }
        private string[] _labels;

        public string[] Labels
        {
            get => _labels;

            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }
        private ISeries[] _seriesThanhToan;

        public ISeries[] SeriesThanhToan
        {
            get => _seriesThanhToan;

            set
            {
                _seriesThanhToan = value;
                OnPropertyChanged();
            }
        }
        private Axis[] _xAxes;

        public Axis[] XAxes
        {
            get => _xAxes;

            set
            {
                _xAxes = value;
                OnPropertyChanged();
            }
        }





    }
}
