using System;
using System.Collections.ObjectModel;
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
using 记账.Model;
using 记账.window;

namespace 记账.Pages
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Goods : Page
    {

        ObservableCollection<Good> dataList = new ObservableCollection<Good>();
        public Goods()
        {
            InitializeComponent();
            GoodsGrid.ItemsSource = dataList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Good good = new Good
            {
                GoodName = "咸菜",
                Unit = "件",
                SellPrice = 60,
                InPrice = 60,
                Remarks = "通常价格",
                IsEnabled = true
            };
            Window addGoods = new AddGoods(good);
            addGoods.Show();
            addGoods.Closed += AddGoods_Closed;
        }

        private void AddGoods_Closed(object sender, EventArgs e)
        {
            Good NewGood = ((AddGoods)sender).WindowGood;
            if (NewGood != null)
            {
                dataList.Add(NewGood);
            }
   
        }
    }
}
