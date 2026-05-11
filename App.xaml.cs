using QL_HaiSan_HoangNhi.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace QL_HaiSan_HoangNhi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppDbContext Db = new AppDbContext();
    }

}
