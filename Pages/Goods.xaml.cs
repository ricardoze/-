using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using 记账.Service;
using 记账.window;

namespace 记账.Pages
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Goods : Page
    {
       

        ObservableCollection<Good> dataList;
        ObservableCollection<String> GoodTypes;
        public Goods()
        {
            InitializeComponent();
            loadGoodTypes();
            refreshGoods("全部");


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Good good = new Good();
            Window addGoods = new AddGoods(good);
            addGoods.Show();
            addGoods.Closed += AddGoods_Closed;
        }

        private void AddGoods_Closed(object sender, EventArgs e)
        {

            //TODO:刷新逻辑有问题
            Good NewGood = ((AddGoods)sender).WindowGood;
          
            refreshGoods();

        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string goodId = button.Uid;

            Good good = Util.ToObject<Good>(new GoodService().GetGoodById(goodId));

            Window addGoods = new AddGoods(good);
            addGoods.Title = "编辑商品";
            addGoods.Show();
            addGoods.Closed += AddGoods_Closed;
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string goodId = button.Uid;

            Good good = Util.ToObject<Good>(new GoodService().GetGoodById(goodId));
            if (good.IsEnabled == 1)
            {
                good.IsEnabled = 0;
            }
            else {
                good.IsEnabled = 1;
            }
            new GoodService().UpdateGood(good);

            refreshGoods(goodTypes.SelectedItem.ToString());
        }
        private void refreshGoods(string goodType=null)
        {
            if (goodType == "全部" || string.IsNullOrEmpty(goodType))
            {
                dataList = Util.ToObservableCollection<Good>(new GoodService().GetGoods());

            }
            else 
            {
                dataList = Util.ToObservableCollection<Good>(new GoodService().GetGoodsByType(goodType));

            }


            GoodsGrid.ItemsSource = dataList;


          
        }

        private void loadGoodTypes()
        {
            GoodTypes = new GoodService().loadGoodTypes();

            goodTypes.ItemsSource = GoodTypes;
            if(goodTypes.SelectedIndex < 0)
            {
                goodTypes.SelectedIndex = 0;
            }
            
        }

        private void goodTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshGoods(goodTypes.SelectedItem.ToString());
            GoodsGrid.ItemsSource = dataList;
        }
    }
   
}