using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godspeed
{
    public class Database
    {
        internal protected static List<Book> Books;
        internal protected static List<Reader> Readers;
        public static void DatabaseInitialisation() { Books = new List<Book>(); Readers = new List<Reader>(); }
        public static IEnumerable<State> States { get; set; } = Enum.GetValues(typeof(State)).Cast<State>();
        public static bool AddReader(string ID, string Full_name)
        {
            try { Readers.Add(new Reader(ID, Full_name)); return true; }
            catch { return false; }
        }
        public static bool AddBook(string Article, string Title, string Genre, string Description, DateTime Book_issuance, DateTime Book_delivery, State State, string Reader)
        {
            try
            {
                bool reader_exists = false;
                foreach (Reader reader in Readers) if (reader.ID == Reader) { reader_exists = true; break; }
                if (!reader_exists) { Reader = null; }
                Books.Add(new Book(Article, Title, Genre, Description, State));
                Books.Last().Book_issuance = Book_issuance;
                Books.Last().Book_delivery = Book_delivery;
                Books.Last().Reader = Reader;
                return true;
            }
            catch { return false; }
        }
        public static bool RedactBook(string Article, string Title, string Genre, string Description, DateTime Book_issuance, DateTime Book_delivery, State State, string Reader)
        {
            try
            {
                Book book = null;
                foreach (Book book_ in Books) if (book_.Article == Article) { book = book_; break; }
                if (book != null)
                {
                    bool is_reader_exists = false;
                    foreach (Reader reader in Readers) if (reader.ID == Reader) { is_reader_exists = true; break; }
                    if (!is_reader_exists) { Reader = null; }
                    book.Title = Title;
                    book.Genre = Genre;
                    book.Description = Description;
                    book.Book_issuance = Book_issuance;
                    book.Book_delivery = Book_delivery;
                    book.State = State;
                    book.Reader = Reader;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        public static bool DeleteBook(string Article)
        {
            try
            {
                foreach (Book book in Books) if (book.Article == Article) { Books.Remove(book); return true; }
                return false;
            }
            catch { return false; }
        }
        public static List<Book> GetBooks() => new List<Book>(Books);
        public static List<Reader> GetReaders() => new List<Reader>(Readers);
        public static List<Book> FindBooks(string Title, string Genre)
        {
            try
            {
                List<Book> Books_find = new List<Book>(Books);
                if (Title != null) foreach (Book book in Books_find) if (!book.Title.Contains(Title)) Books_find.Remove(book);
                if (Genre != null) foreach (Book book in Books_find) if (!book.Genre.Contains(Genre)) Books_find.Remove(book);
                return Books_find;
            }
            catch { return null; }
        }
    }
    public class Book
    {
        public string Article { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime Book_issuance { get; set; }
        public DateTime Book_delivery { get; set; }
        public State State { get; set; }
        public string Reader { get; set; }
        public Book(string article, string title, string genre, string description, State state)
        {
            Article = article;
            Title = title;
            Genre = genre;
            Description = description;
            State = state;
        }
    }
    public enum State
    {
        In_stock,
        Issued,
        In_repair
    }
    public class Reader
    {
        public string ID { get; set; }
        public string Full_name { get; set; }
        public Reader(string ID, string Full_name)
        {
            this.ID = ID;
            this.Full_name = Full_name;
        }
    }
}
