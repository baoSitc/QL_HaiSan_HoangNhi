using QL_HaiSan_HoangNhi.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QL_HaiSan_HoangNhi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            // load màn đầu tiên
            OpenTab(
         "Quản lý hàng hóa",
         "OfficeBuilding",
         new HangHoaView());

        }
        private void OpenTab(string title,
                     string iconKind,
                     UserControl control)
        {
            foreach (TabItem item in MainTabControl.Items)
            {
                StackPanel header = item.Header as StackPanel;

                if (header != null)
                {
                    TextBlock txt = header.Children[1] as TextBlock;

                    if (txt != null && txt.Text == title)
                    {
                        MainTabControl.SelectedItem = item;
                        return;
                    }
                }
            }

            TabItem tab = new TabItem();

            tab.Header = CreateTabHeader(title, iconKind);

            tab.Content = control;

            MainTabControl.Items.Add(tab);

            MainTabControl.SelectedItem = tab;
        }
        private StackPanel CreateTabHeader(string title, string iconKind)
        {
            StackPanel sp = new StackPanel();

            sp.Orientation = Orientation.Horizontal;

            var icon = new MaterialDesignThemes.Wpf.PackIcon();

            icon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)
                Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), iconKind);

            icon.Width = 18;
            icon.Height = 18;
            icon.Margin = new Thickness(0, 0, 5, 0);

            TextBlock txt = new TextBlock();

            txt.Text = title;

            sp.Children.Add(icon);

            sp.Children.Add(txt);

            return sp;
        }
        private void BtnHangHoa_Click(object sender, RoutedEventArgs e)
        {
            OpenTab(
         "Quản lý hàng hóa",
         "PackageVariantClosed",
         new HangHoaView());
        }
        private void BtnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            OpenTab(
         "Quản lý khách hàng",
         "PackageVariantClosed",
         new KhachHangView());
        }
        private void BtnBanHang_Click(object sender, RoutedEventArgs e)
        {
            OpenTab(
         "Bán hàng",
         "CashRegister",
         new BanHangView());
        }
    }
}