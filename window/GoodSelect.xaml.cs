using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 记账.Model;
using 记账.Service;

namespace 记账.window
{
    /// <summary>
    /// GoodSelect.xaml 的交互逻辑
    /// </summary>
    public partial class GoodSelect : Window
    {
        public GoodSelect()
        {
            InitializeComponent();
            loadGoodTypes();
            refreshGoods("全部");
        }
        ObservableCollection<Good> dataList;
        ObservableCollection<String> GoodTypes;
        private void goodTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshGoods(goodTypes.SelectedItem.ToString());
            GoodsGrid.ItemsSource = dataList;
        }

        private void refreshGoods(string goodType = null)
        {
            if (goodType == "全部")
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
            if (goodTypes.SelectedIndex < 0)
            {
                goodTypes.SelectedIndex = 0;
            }

        }

    }
}
