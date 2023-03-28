using System;
using System.Collections.Generic;
using System.Xml;

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

        

    }
}
