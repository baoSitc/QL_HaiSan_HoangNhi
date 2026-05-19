using CommunityToolkit.Mvvm.Input;
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

    public partial class BanHangViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //Tạo các tab hóa đơn tạm
        public ObservableCollection<HoaDonTamViewModel>
    DanhSachHoaDonTam
        {
            get;
            set;
        }
    = new();
        //tab hiện tại
        private HoaDonTamViewModel _selectedHoaDon;

        public HoaDonTamViewModel SelectedHoaDon
        {
            get => _selectedHoaDon;
            set
            {
                _selectedHoaDon = value;

                OnPropertyChanged();
            }
        }
        //thêm tab hóa đơn mới
        public ICommand TaoHoaDonCommand
        {
            get;
        }

        //tìm kiếm hàng hóa
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
                LoadHangHoa();
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
        public Loaihang _selectedLoaiHang;

        public ObservableCollection<Loaihang> DanhSachLoaiHang
        {
            get;
            set;
        }
        public Loaihang SelectedLoaiHang
        {
            get => _selectedLoaiHang;
            set
            {
                _selectedLoaiHang = value;

                // Load hàng hóa based on selected loại hàng
                LoadHangHoa();

                OnPropertyChanged();
            }
        }
        public void LoadHangHoa()
        {
            // TẤT CẢ
            if (SelectedLoaiHang == null
         || SelectedLoaiHang.Id == 0)
            {
                DanhSachHangHoa =
                    new ObservableCollection<Hanghoa>
                (
                    App.Db.Hanghoas.OrderBy(x => x.Tenhh).ToList()
                );

                return;
            }

            DanhSachHangHoa =
                new ObservableCollection<Hanghoa>
            (
                App.Db.Hanghoas
                    .Where(x =>
                        x.LoaihangId == SelectedLoaiHang.Id)
                    .OrderBy(x => x.Tenhh)
                    .ToList()
            );
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
                else
                {
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
        public ICommand TangSoLuongCommand
        {
            get;
            set;
        }

        public ICommand GiamSoLuongCommand
        {
            get;
            set;
        }

        //Contructor
        public BanHangViewModel()
        {
            LoadKhachHang();
            //load loại hàng
            LoadLoaiHang();
            TaoHoaDonCommand =
      new RelayCommand(TaoHoaDon);
            LoadHoaDonTam();

            // nếu không có hóa đơn nào
            if (DanhSachHoaDonTam.Count == 0)
            {
                TaoHoaDon();
            }



            ThemGioHangCommand = new RelayCommand(ThemGioHang);
            ChonHangCommand = new RelayCommand<Hanghoa>(ChonHang);
            TangSoLuongCommand =
                new RelayCommand<HoaDonItem>(TangSoLuong);

            GiamSoLuongCommand =
                new RelayCommand<HoaDonItem>(GiamSoLuong);

        }



        public void TangSoLuong(HoaDonItem item)
        {
            if (item == null)
                return;

            item.SoLuong++;

            UpdateCTHoaDon(item);

            TinhTongTien();
        }
        public void GiamSoLuong(HoaDonItem item)
        {
            if (item == null)
                return;

            item.SoLuong--;

            // <= 0 => xóa
            if (item.SoLuong <= 0)
            {
                XoaHang(item);

                return;
            }

            UpdateCTHoaDon(item);

            TinhTongTien();
        }
        public void XoaHang(HoaDonItem item)
        {
            // xóa grid
            SelectedHoaDon.GioHang.Remove(item);

            // xóa DB
            var ct = App.Db.CtHoadons
                .FirstOrDefault(x =>
                    x.HoadonId == SelectedHoaDon.HoaDonId
                    && x.HanghoaId == item.HangHoaId);

            if (ct != null)
            {
                App.Db.CtHoadons.Remove(ct);

                App.Db.SaveChanges();
            }

            TinhTongTien();
        }
        public void CloseTab(HoaDonTamViewModel tab)
        {
            if (tab == null)
                return;

            DanhSachHoaDonTam.Remove(tab);

            // tạo tab mới nếu hết
            if (DanhSachHoaDonTam.Count == 0)
            {
                TaoHoaDon();
            }
        }
        public void TaoHoaDon()
        {
            String _tenTab = $"Đơn {DanhSachHoaDonTam.Count + 1}";
            if (TenKhachHang != null && TenKhachHang != string.Empty)
            {
                _tenTab = TenKhachHang;
            }

            HoaDonTamViewModel hd =
                new HoaDonTamViewModel()

                { TenTab = _tenTab };
            hd.CloseTabAction = CloseTab;



            DanhSachHoaDonTam.Add(hd);

            SelectedHoaDon = hd;
        }

        public void LoadLoaiHang()
        {
            var data = App.Db.Loaihangs.ToList();

            // thêm dòng Tất cả
            data.Insert(0, new Loaihang()
            {
                Id = 0,
                Tenloai = "Tất cả"
            });

            DanhSachLoaiHang =
                new ObservableCollection<Loaihang>(data);

            SelectedLoaiHang = DanhSachLoaiHang.FirstOrDefault();
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
            if (SelectedHoaDon == null) return;
            SelectedHoaDon.TongTien = SelectedHoaDon.GioHang.Sum(x => x.ThanhTien);
            // update DB
            var hd = App.Db.Hoadons
                .FirstOrDefault(x =>
                    x.Id == SelectedHoaDon.HoaDonId);

            if (hd != null)
            {
                hd.Tongtien = SelectedHoaDon.TongTien;

                App.Db.SaveChanges();
            }

            OnPropertyChanged(nameof(SelectedHoaDon.GioHang));
        }
        public void ChonHang(Hanghoa hh)
        {

            if (hh == null || SelectedHoaDon == null)
                return;
            //Kiểm tra khách hàng đã có chưa, nếu chưa có thì thêm mới, nếu có rồi thì tăng số lượng
            var kh = SelectedHoaDon.KiemTraKhachHang();
            if (kh == null) return;


            // tạo hóa đơn tạm
            TaoHoaDonTam();

            // chưa tạo được
            if (SelectedHoaDon.HoaDonId <= 0)
                return;


            var item = SelectedHoaDon.GioHang
         .FirstOrDefault(x => x.HangHoaId == hh.Id);

            if (item != null)
            {
                item.SoLuong++;
                UpdateCTHoaDon(item);

                TinhTongTien();

                return;
            }

            HoaDonItem newItem = new HoaDonItem()
            {
                HangHoaId = hh.Id,
                TenHang = hh.Tenhh,
                DonGia = hh.Giaban ?? 0,
                SoLuong = 1,
                Dvt = hh.Dvt
            };

            // QUAN TRỌNG
            newItem.PropertyChanged += Item_PropertyChanged;

            SelectedHoaDon.GioHang.Add(newItem);
            // save DB
            InsertCTHoaDon(newItem);


            TinhTongTien();
            // LoadKhachHang();
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {

            if (sender is HoaDonItem item)
            {
                UpdateCTHoaDon(item);

                TinhTongTien();
            }
        }
        public void TaoHoaDonTam()
        {
            if (SelectedHoaDon == null)
                return;

            // đã có bill
            if (SelectedHoaDon.HoaDonId > 0)
                return;

            var kh = SelectedHoaDon.KiemTraKhachHang();

            if (kh == null)
                return;

            Hoadon hd = new Hoadon()
            {
                Ngaylap = DateTime.Now,
                KhachhangId = kh.Id,
                ShipperId = 1,
                NhanvienId = 1,
                Tongtien = 0,
                Trangthai = "TAM",
                Diachigiao = SelectedHoaDon.DiaChiGiao,
                Ghichu = SelectedHoaDon.GhiChu

            };
            //lấy tên tab là 
            SelectedHoaDon.TenTab = kh.Tenkh;


            App.Db.Hoadons.Add(hd);

            App.Db.SaveChanges();

            // lưu lại ID DB
            SelectedHoaDon.HoaDonId = hd.Id;
        }
        public void InsertCTHoaDon(HoaDonItem item)
        {
            CtHoadon ct = new CtHoadon()
            {
                HoadonId = SelectedHoaDon.HoaDonId,
                HanghoaId = item.HangHoaId,
                Soluong = item.SoLuong,
                Dongia = item.DonGia,
                Thanhtien = item.ThanhTien
            };

            App.Db.CtHoadons.Add(ct);

            App.Db.SaveChanges();
        }
        public void UpdateCTHoaDon(HoaDonItem item)
        {
            var ct = App.Db.CtHoadons
                .FirstOrDefault(x =>
                    x.HoadonId == SelectedHoaDon.HoaDonId
                    && x.HanghoaId == item.HangHoaId);

            if (ct == null)
                return;

            ct.Soluong = item.SoLuong;
            ct.Dongia = item.DonGia;

            ct.Thanhtien = item.ThanhTien;

            App.Db.SaveChanges();
        }

        //load hóa đơn tạm vào các tab
        public void LoadHoaDonTam()
        {
            var listHoaDon = App.Db.Hoadons
                .Where(x => x.Trangthai == "TAM")
                .ToList();

            foreach (var hd in listHoaDon)
            {
                HoaDonTamViewModel tab =
                    new HoaDonTamViewModel();
                tab.CloseTabAction = CloseTab;

                // =========================
                // THÔNG TIN HÓA ĐƠN
                // =========================

                tab.HoaDonId = hd.Id;

                tab.TongTien = hd.Tongtien ?? 0;

                tab.TenTab = $"HD {hd.Id}";

                // =========================
                // KHÁCH HÀNG
                // =========================

                var kh = App.Db.Khachhangs
                    .FirstOrDefault(x => x.Id == hd.KhachhangId);

                if (kh != null)
                {
                    // tab.SelectedKhachHang = kh;

                    tab.SoDienThoai = kh.Sdt;

                    tab.TenKhachHang = kh.Tenkh;

                    tab.DiaChiGiao = kh.Diachi;
                    tab.TenTab = kh.Tenkh;
                }

                // =========================
                // CHI TIẾT HÓA ĐƠN
                // =========================

                var ctList = App.Db.CtHoadons
                    .Where(x => x.HoadonId == hd.Id)
                    .ToList();

                foreach (var ct in ctList)
                {
                    var hh = App.Db.Hanghoas
                        .FirstOrDefault(x => x.Id == ct.HanghoaId);

                    HoaDonItem item = new HoaDonItem()
                    {
                        HangHoaId = ct.HanghoaId ?? 0,

                        TenHang = hh?.Tenhh,

                        Dvt = hh?.Dvt,

                        SoLuong = ct.Soluong ?? 0,

                        DonGia = ct.Dongia ?? 0
                    };

                    // realtime update
                    item.PropertyChanged += Item_PropertyChanged;

                    tab.GioHang.Add(item);
                }

                // add tab
                DanhSachHoaDonTam.Add(tab);
            }

            // chọn tab đầu
            SelectedHoaDon =
                DanhSachHoaDonTam.FirstOrDefault();
        }



    }
}
