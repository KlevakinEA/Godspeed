using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Godspeed
{
    /// <summary>
    /// Логика взаимодействия для ReaderSelection.xaml
    /// </summary>
    public partial class ReaderSelection : Window
    {
        public static Reader SelectedReader;
        public ReaderSelection()
        {
            InitializeComponent();
            MainWindow.ReadersUpdate();
            ReadersList.ItemsSource = MainWindow.Readers;
            ReadersList.Items.Refresh();
        }
        private void SelectClick(object sender, RoutedEventArgs e)
        {
            SelectedReader = (Reader)ReadersList.SelectedItem;
            Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            SelectedReader = null;
            Close();
        }
    }
}
