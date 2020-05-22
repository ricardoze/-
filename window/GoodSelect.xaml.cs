using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public OrderDetail detail;
        public GoodSelect()
        {
      
            InitializeComponent();
            loadGoodTypes();
            refreshGoods("全部");
            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(1, 100));
            numberBox.ItemsSource = LongListToTestComboVirtualization;

        }
        public IList<int> LongListToTestComboVirtualization { get; }
        ObservableCollection<Good> dataList;
        ObservableCollection<String> GoodTypes;
        public Good SelectGood;

        private void goodTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshGoods(goodTypes.SelectedItem.ToString());
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
            GoodsGrid.ItemsSource = new ObservableCollection<Good>(dataList.OrderBy(item => item.GoodName)); 

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

      
        private void closeclick(object sender, RoutedEventArgs e)
        {
            detail = null;
            this.Close();
        } 
        
        private void finishclick(object sender, RoutedEventArgs e)
        {
            if (SelectGood == null)
            {
                SnackbarSeven.MessageQueue.Enqueue(
               $"请选择商品",
               null,
               null,
               null,
               false,
               true,
               TimeSpan.FromSeconds(1));

                return;

            }
            string goodId = SelectGood.Id;

            object goodCount = numberBox.SelectedValue;
            if (goodCount == null)
            {
                SnackbarSeven.MessageQueue.Enqueue(
               $"请选择数量",
               null,
               null,
               null,
               false,
               true,
               TimeSpan.FromSeconds(1));

               return;

            }
            if (string.IsNullOrEmpty(goodCount.ToString()) || string.IsNullOrEmpty(goodId))
            {
                SnackbarSeven.MessageQueue.Enqueue(
             $"请选择商品和数量",
             null,
             null,
             null,
             false,
             true,
             TimeSpan.FromSeconds(1));

                return;
            }

            detail = new OrderDetail()
            {
                GoodCount = int.Parse(goodCount.ToString()),
                GoodId = goodId
            };
            this.Close();
        }

        private void GoodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectGood = (Good)this.GoodsGrid.SelectedItem;
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
