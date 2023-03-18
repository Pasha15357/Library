using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Библиотека
{
    internal class BookInterface //класс интерфейса книг, создающий меню работы с книгами
    {
        public static void BookMenu() //метод главного меню по работе с книгами
        {
            ICollection<Book> books = new List<Book>(); //создаем коллекцию книг

            int id = 0; //задаем пустой ID, чтобы переназначить его в методах

            
            

            Console.Clear(); //очищаем полностью консоль
            Console.WriteLine("                      ===============================\n" +
                              "                      |        1. Список книг       |\n" +
                              "                      ===============================\n" +
                              "                      |       2. Добавить книгу     |\n" +
                              "                      ===============================\n" +
                              "                      |       3. Удалить книгу      |\n" +
                              "                      ===============================\n" +
                              "                      |    4. Выход в главное меню  |\n" +
                              "                      ===============================");
            Console.WriteLine();
            Console.Write("Введите код операции:  ");
            char Code = Console.ReadKey(true).KeyChar; //считываем введенный код и переходим по соответствующему меню
            switch (Code)
            {
                case '1':
                    ShowBooks(ref books); //при вводе 1 демонстрируются все книги
                    break;
                case '2':
                    AddBook(ref books, ref id); //при вводе 2 осуществляется переход в меню с добавлением книги
                    break;
                case '3':
                    DeleteBook(ref books); //при вводе 3 осуществляется переход в меню с удалением книги
                    break;
                case '4':
                    Main.MainMenu(); //при вводе 4 осуществляется переход в главное меню приложения
                    break;
                default: 
                    Console.WriteLine("Неверный код операции, введите еще раз: "); //при вводе любого другого числа дается попытка ввода числа еще раз
                    Console.ReadKey();
                    BookMenu();
                    break;
            }
        }

        private static void ShowBooks(ref ICollection<Book> books) //метод по выводу всех книг
        {
            using (StreamReader reader = new("books.txt")) //присваеваем потоку чтения файл с книгами
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    books.Add(Book.FromString(reader.ReadLine())); //применяем метод из Book.cs по чтению строки и добавляем книгу в колллекцию                   
                }
            }
            Console.Clear(); //очищаем полностью консоль
            if (books.Count() != 0) //если книги есть
            {
                foreach (Book book in books) //выводим каждую книгу колллекции
                {
                    book.Show(); //метод по выводу одной книги
                }
            }
            else //если число книг равно 0
            {
                Console.WriteLine("Книг нет");
            }
            Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
            Console.ReadKey();
            BookMenu(); //возвращаемся в меню работы с книгами
        }

        static void AddBook(ref ICollection<Book> books, ref int id) //метод по добавлению книги
        {
            using (StreamReader reader = new StreamReader("books.txt")) //присваеваем потоку чтения файл с книгами
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    books.Add(Book.FromString(reader.ReadLine())); //применяем метод из Book.cs по чтению строки и добавляем книгу в колллекцию
                    if (books == null) //если книг нет
                    {
                        id = 0; //первый ID=0
                    }
                    else //иначе к последнему добавляем 1, чтобы следующая книга была с ID на 1 больше
                    {
                        id = books.Last().Id + 1;
                    }
                }
            }
            Console.Clear(); //очищаем полностью консоль
            try
            {
                Console.WriteLine("Введите название книги");
                string Title = Console.ReadLine();
                Console.WriteLine("Введите автора книги");
                string Author = Console.ReadLine();
                Console.WriteLine("Введите цену");
                int Price = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите жанр книги");
                string Genre = Console.ReadLine();
                Console.WriteLine("Введите год");
                int Year = int.Parse(Console.ReadLine());
                Book book = new(id, Title, Author, Price, Genre, Year); //применяем конструктор из Book.cs и записываем все введенные данные в новую книгу
                books.Add(book); //добавляем в коллекцию новую книгу
            }
            catch 
            {
                Console.WriteLine("Ошибка! Неверный формат");
                Console.WriteLine("Нажмите любую клавишу, чтобы ввести заново");
                Console.ReadKey();
                BookMenu();
            }
            
            using (StreamWriter writer = new("books.txt", false)) //создаем новый поток записи, который запишет новую книгу в файл
            {                
                foreach (Book _book in books)
                {
                    writer.WriteLine(_book.ToString()); //применяем метод из Book.cs по записи метода файл к каждой книге
                }

            }
            BookMenu(); //возвращаемся в меню работы с книгами
        }

        static void DeleteBook(ref ICollection<Book> books) //метод по удалению книги
        {

            using (StreamReader reader = new("books.txt")) //присваеваем потоку чтения файл с книгами
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    books.Add(Book.FromString(reader.ReadLine())); //применяем метод из Book.cs по чтению строки и добавляем книгу в колллекцию                   
                }
            }
            Console.Clear(); //очищаем полностью консоль
            foreach (Book book in books) //выводим каждую книгу колллекции
            {
                book.Show(); //метод по выводу одной книги
            }
            if (books.Count == 0) //если число книг равно 0
            {
                Console.WriteLine("Книг нет.");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                BookMenu(); //возвращаемся в меню работы с книгами
            }
            else
            {
                try
                {
                    Console.WriteLine("Введите код записи, которую хотите удалить: ");
                    int id = int.Parse(Console.ReadLine());
                    var temp = books.Where(d => d.Id == id).First(); //проходимся по всей коллекции, пока не встретим книгу с введенным ID и записываем ее в переменную
                    books.Remove(temp); //удаляем из коллекции
                    using (StreamWriter writer = new("books.txt")) //создаем новый поток записи, который удалит книгу из файла
                    {
                        foreach (Book _book in books)
                        {
                            writer.WriteLine(_book.ToString()); //записываем в файл пустое значение вместо удаленной книги
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Такой книги не существует!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы выйти в меню");
                    Console.ReadKey();
                    BookMenu();
                }
                BookMenu(); //возвращаемся в меню работы с книгами


            }
        }
    }
}
