using System;

namespace Библиотека
{
    // Класс книги
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public Book(int id, string title, string author, decimal price, string genre, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
            Genre = genre;
            Year = year;
        }

        // Метод по выводу книги
        public void Show()
        {
            Console.WriteLine("=======================Книга======================\n" +
                                      $"Код книги: {Id}\n" +
                                      $"Название книги: {Title}\n" +
                                      $"Автор книги: {Author}\n" +
                                      $"Цена книги: {Price}\n" +
                                      $"Жанр книги: {Genre}\n" +
                                      $"Год издания книги: {Year}\n" +
                                      "==================================================\n");
        }

        // Функция преобразования книги в строку для записи в файл
        public override string ToString()
        {
            return $"{Id}|{Title}|{Author}|{Price}|{Genre}|{Year}";
        }

        // Функция чтения книги из строки в файле
        public static Book FromString(string str)
        {
            string[] fields = str.Split('|');
            return new Book(int.Parse(fields[0]), fields[1], fields[2], decimal.Parse(fields[3]), fields[4], int.Parse(fields[5]));
        }
    }
}
