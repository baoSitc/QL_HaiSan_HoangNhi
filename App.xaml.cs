using QL_HaiSan_HoangNhi.Models;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Globalization;
using System.Threading;

namespace QL_HaiSan_HoangNhi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppDbContext Db = new AppDbContext();
        public App()
        {
            var culture = new CultureInfo("vi-VN");

            culture.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = culture;

            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }


}
