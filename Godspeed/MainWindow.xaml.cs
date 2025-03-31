using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Godspeed
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Book> Books { get; set; }
        public static ObservableCollection<Book> FindBooks { get; set; }
        public static ObservableCollection<Reader> Readers { get; set; }
        public MainWindow()
        {
            Database.DatabaseInitialisation();
            Books = new ObservableCollection<Book>();
            FindBooks = new ObservableCollection<Book>();
            Readers = new ObservableCollection<Reader>();
            Database.AddReader("1", "Name1");
            Database.AddReader("2", "Name2");
            Database.AddReader("3", "Name3");
            InitializeComponent();
            BooksUpdate();
            ReadersUpdate();
        }
        public void BooksUpdate()
        {
            Books.Clear();
            Books = new ObservableCollection<Book>(Database.GetBooks());
            BooksList.ItemsSource = Books;
            BooksList.Items.Refresh();
        }
        public static void ReadersUpdate()
        {
            Readers.Clear();
            Readers = new ObservableCollection<Reader>(Database.GetReaders());
        }

        private void FindClick(object sender, RoutedEventArgs e)
        {
            FindBooks.Clear();
            List<Book> books = Database.FindBooks(TitleBox.Text, GenreBox.Text);
            if (books != null)
            {
                FindBooks = new ObservableCollection<Book>(books);
                FindList.ItemsSource = FindBooks;
                FindList.Items.Refresh();
            }
            else
            {
                FindBooks = new ObservableCollection<Book>();
                FindList.ItemsSource = FindBooks;
                FindList.Items.Refresh();
            }
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem != null)
            {
                if (Database.DeleteBook((BooksList.SelectedItem as Book).Article)) { BooksUpdate(); MessageBox.Show("Deletion successful."); }
                else MessageBox.Show("How th did you even do that?");
            }
            else MessageBox.Show("No book selected.");
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem != null)
            {
                Entry_redaction entry_Redaction = new Entry_redaction((Book)BooksList.SelectedItem);
                entry_Redaction.ShowDialog();
                BooksUpdate();
            }
            else MessageBox.Show("No book selected.");
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Entry_redaction entry_Redaction = new Entry_redaction(null);
            entry_Redaction.ShowDialog();
            BooksUpdate();
        }
    }
}