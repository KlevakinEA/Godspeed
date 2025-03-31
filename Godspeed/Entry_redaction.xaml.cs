using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Godspeed
{
    /// <summary>
    /// Логика взаимодействия для Entry_redaction.xaml
    /// </summary>
    public partial class Entry_redaction : Window
    {
        private protected Book Book;
        private protected Reader SelectedReader;
        public Entry_redaction(Book book)
        {
            InitializeComponent();
            if (book != null)
            {
                Book = book;
                ArticleBox.Text = book.Article;
                TitleBox.Text = book.Title;
                GenreBox.Text = book.Genre;
                DescBox.Text = book.Description;
                IssuingBox.SelectedDate = book.Book_issuance;
                DeliveryBox.SelectedDate= book.Book_delivery;
                StateBox.SelectedItem = book.State;
                if (book.Reader == null) ReaderBox.Text = null;
                else foreach (Reader r in MainWindow.Readers) if (r.ID == book.Reader) { SelectedReader = r; ReaderBox.Text = r.Full_name; break; }
            }
        }
        private void SelectClick(object sender, RoutedEventArgs e)
        {
            ReaderSelection readerSelection = new ReaderSelection();
            readerSelection.ShowDialog();
            SelectedReader = ReaderSelection.SelectedReader;
            if (SelectedReader == null) ReaderBox.Text = null;
            else ReaderBox.Text = SelectedReader.Full_name;
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Book == null)
            {
                if ((ArticleBox.Text != string.Empty) && (TitleBox.Text != string.Empty) && (GenreBox.Text != string.Empty) && (DescBox.Text != string.Empty) && (StateBox.SelectedItem != null))
                {
                    try
                    {
                        if (SelectedReader != null) if (Database.AddBook(ArticleBox.Text, TitleBox.Text, GenreBox.Text, DescBox.Text, (DateTime)IssuingBox.SelectedDate, (DateTime)DeliveryBox.SelectedDate, (State)StateBox.SelectedItem, SelectedReader.ID)) Close();
                        else if (Database.AddBook(ArticleBox.Text, TitleBox.Text, GenreBox.Text, DescBox.Text, (DateTime)IssuingBox.SelectedDate, (DateTime)DeliveryBox.SelectedDate, (State)StateBox.SelectedItem, null)) Close();
                    }
                    catch { MessageBox.Show("Some error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                else MessageBox.Show("One of the essential boxes is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if ((TitleBox.Text != string.Empty) && (GenreBox.Text != string.Empty) && (DescBox.Text != string.Empty) && (StateBox.SelectedItem != null))
                {
                    try
                    {
                        if (SelectedReader != null) if (Database.RedactBook(Book.Article, TitleBox.Text, GenreBox.Text, DescBox.Text, (DateTime)IssuingBox.SelectedDate, (DateTime)DeliveryBox.SelectedDate, (State)StateBox.SelectedItem, SelectedReader.ID)) Close();
                        else if (Database.RedactBook(Book.Article, TitleBox.Text, GenreBox.Text, DescBox.Text, (DateTime)IssuingBox.SelectedDate, (DateTime)DeliveryBox.SelectedDate, (State)StateBox.SelectedItem, null)) Close();
                    }
                    catch { MessageBox.Show("Some error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                else MessageBox.Show("One of the eccential boxes is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e) => Close();
    }
}
