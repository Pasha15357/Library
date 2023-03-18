using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Библиотека
{
    internal class LendingInterface //класс интерфейса выдачи книг, создающий меню работы с выдачами книг
    {
        public static void LendingMenu() //метод меню по работе с выдачами книг

        {

            ICollection<Lending> lendings = new List<Lending>(); //создаем коллекцию выдач книг
            int id =0; //задаем пустой ID, чтобы переназначить его в методах
             
            

            ICollection<Book> books = new List<Book>(); //создаем коллекцию книг

            using (StreamReader reader = new StreamReader("books.txt")) //присваеваем потоку чтения файл с книгами
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    books.Add(Book.FromString(reader.ReadLine())); //применяем метод из Book.cs по чтению строки и добавляем книгу в колллекцию     
                }
            }

            ICollection<Reader> readers = new List<Reader>();

            using (StreamReader reader = new StreamReader("readers.txt")) //присваеваем потоку чтения файл с читателями
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    readers.Add(Reader.FromString(reader.ReadLine())); //применяем метод из Reader.cs по чтению строки и добавляем читателя в колллекцию 
                }
            }


            Console.Clear(); //очищаем полностью консоль
            Console.WriteLine("                      ===============================\n" +
                              "                      |     1. Список выдач книг    |\n" +
                              "                      ===============================\n" +
                              "                      |   2. Добавить выдачу книг   |\n" +
                              "                      ===============================\n" +
                              "                      |    3. Удалить выдачу книг   |\n" +
                              "                      ===============================\n" +
                              "                      |   4. Выход в главное меню   |\n" +
                              "                      ===============================");
            Console.WriteLine();
            Console.Write("Введите код операции:  ");
            char Code = Console.ReadKey(true).KeyChar; //считываем введенный код и переходим по соответствующему меню
            switch (Code)
            {
                case '1':
                    ShowLendings(readers, books, lendings); //при вводе 1 демонстрируются все выдачи книг
                    break;
                case '2':
                    AddLending(readers, books, ref lendings, ref id); //при вводе 2 осуществляется переход в меню с добавлением выдачи книги
                    break;
                case '3':
                    DeleteLending(ref lendings); //при вводе 3 осуществляется переход в меню с удалением выдачи книги
                    break;
                case '4':
                    Main.MainMenu(); //при вводе 4 осуществляется переход в главное меню приложения
                    break;
                default:
                    Console.WriteLine("Неверный код операции"); //при вводе любого другого числа дается попытка ввода числа еще раз
                    Console.ReadKey();
                    LendingMenu();
                    break;
            }
        }

        internal static void ShowLendings(ICollection<Reader> readers, ICollection<Book> books, ICollection<Lending> lendings) //метод по выводу всех выдач книг
        {
            using (StreamReader reader = new StreamReader("lendings.txt")) //присваеваем потоку чтения файл с выдачами книг
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    lendings.Add(Lending.FromString(reader.ReadLine())); //применяем метод из Lending.cs по чтению строки и добавляем выдачу книги в колллекцию
                }
            }
            Console.Clear(); //очищаем полностью консоль
            if (lendings.Count() != 0) //если выдачи книг есть
            {
                foreach (Lending lending in lendings) //выводим каждую выдачу книги в колллекции
                {
                    lending.Show(); //метод вывода одной выдачи книги
                }
            }
            else //если число выдач книг равно 0
            {
                Console.WriteLine("Выдач книг нет");
            }
            Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
            Console.ReadKey();
            LendingMenu(); //возвращаемся в меню работы с выдачами книг
        }

        static void AddLending(ICollection<Reader> readers, ICollection<Book> books, ref ICollection<Lending> lendings, ref int id) //метод по добавлению выдачи книги
        {
            Console.Clear(); //очищаем полностью консоль
            if (books.Count() == 0) //если книг нет
            {
                Console.WriteLine("Добавьте как минимум 1 книгу, чтобы добавить выдачу книги");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                LendingMenu(); //возвращаемся в меню работы с выдачами книг
            }
            else if (readers.Count() == 0) //если читателей нет
            {
                Console.WriteLine("Добавьте как минимум 1 читателя, чтобы добавить выдачу книги");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                LendingMenu(); //возвращаемся в меню работы с выдачами книг
            }
            else
            {
                using (StreamReader reader = new StreamReader("lendings.txt")) //присваеваем потоку чтения файл с выдачами книг
                {
                    while (!reader.EndOfStream) //пока не достигнут конец файла
                    {
                        lendings.Add(Lending.FromString(reader.ReadLine())); //применяем метод из Lending.cs по чтению строки и добавляем выдачу книги в колллекцию
                        if (lendings == null) //если выдач книг нет
                        {
                            id = 0; //первый ID=0
                        }
                        else //иначе к последнему добавляем 1, чтобы следующая выдача книги была с ID на 1 больше
                        {
                            id = lendings.Last().Id + 1;
                        }
                    }
                }
                foreach (Book book in books) //выводим каждую книгу колллекции
                {
                    book.Show(); //метод вывода одной книги
                }
                Console.WriteLine("Введите код книги, которую возьмет читатель");
                int book_code = int.Parse(Console.ReadLine());                
                try
                {
                    var temp_books = books.Where(d => d.Id == book_code).First(); //проходимся по всей коллекции, пока не встретим книгу с введенным ID и записываем ее в переменную
                    Console.Clear(); //очищаем полностью консоль
                    if (temp_books != null) //если книга есть
                    {
                        foreach (Reader reader in readers) //выводим каждого читателя колллекции
                        {
                            reader.Show(); //метод по выводу одного читателя
                        }
                        Console.WriteLine("Введите код читателя, который возьмет книгу");
                        int reader_code = int.Parse(Console.ReadLine());                        
                        var temp_readers = readers.Where(d => d.Id == reader_code).First(); //проходимся по всей коллекции, пока не встретим читателя с введенным ID и записываем его в переменную
                        
                        if (temp_readers != null) //если читатель есть
                        {
                            string Title = temp_books.Title; //записываем из книги с нашим ID название этой книги в переменную названия книги выдачи книги
                            int TicketNumber = temp_readers.TicketNumber;  //записываем из читателя с нашим ID номер его билета в переменную номера билета выдачи книги
                            Console.Clear(); //очищаем полностью консоль
                            Console.WriteLine("Введите дату выдачи");
                            string IssueDate = Console.ReadLine();
                            Console.WriteLine("Введите срок пользования");
                            string UsagePeriod = Console.ReadLine();
                            Console.WriteLine("Введите имя библиотекаря");
                            string LibrarianName = Console.ReadLine();
                            Lending lending = new(id, Title, TicketNumber, IssueDate, UsagePeriod, LibrarianName); //применяем конструктор из Lending.cs и записываем все введенные данные в новую выдачу книги
                            lendings.Add(lending); //добавляем в коллекцию новую выдачу книги                           

                            using (StreamWriter writer = new StreamWriter("lendings.txt", false)) //создаем новый поток записи, который запишет новую книгу в файл
                            {
                                foreach (Lending _lending in lendings)
                                {
                                    writer.WriteLine(_lending.ToString()); //применяем метод из Lending.cs по записи метода файл к каждой выдаче книги
                                }
                            }
                            LendingMenu(); //возвращаемся в меню работы с выдачами книг
                        }
                    }
                }
                catch 
                {
                    Console.WriteLine("Ошибка! Такой записи не существует!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы выйти в меню");
                    Console.ReadKey();
                    LendingMenu();
                }
                
            }           
        }

        static void DeleteLending(ref ICollection<Lending> lendings) //метод по удалению выдачи книги
        {
            
            using (StreamReader reader = new StreamReader("lendings.txt")) //присваеваем потоку чтения файл с выдачами книг
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    lendings.Add(Lending.FromString(reader.ReadLine())); //применяем метод из Lending.cs по чтению строки и добавляем выдачу книги в колллекцию
                }
            }
            Console.Clear(); //очищаем полностью консоль
            if (lendings.Count == 0) //если число книг равно 0
            {
                Console.WriteLine("Выдач книг нет.");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                LendingMenu(); //возвращаемся в меню работы с выдачей книг
            }
            else
            {
                foreach (Lending lending in lendings) //выводим каждую выдачу книги в колллекции
                {
                    lending.Show(); //метод вывода одной выдачи книги
                }
                try
                {
                    Console.WriteLine("Введите код записи, которую хотите удалить: ");
                    int id = int.Parse(Console.ReadLine());
                    var temp = lendings.Where(d => d.Id == id).First(); //проходимся по всей коллекции, пока не встретим выдачу книги с введенным ID и записываем ее в переменную
                    lendings.Remove(temp); //удаляем из коллекции
                    using (StreamWriter writer = new StreamWriter("lendings.txt")) //создаем новый поток записи, который удалит выдачу книги из файла
                    {
                        foreach (Lending _lending in lendings)
                        {
                            writer.WriteLine(_lending.ToString()); //записываем в файл пустое значение вместо удаленной выдачи книги
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Такой книги не существует!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы выйти в меню");
                    Console.ReadKey();
                    LendingMenu();
                }
                LendingMenu(); //возвращаемся в меню работы с книгами
            }
            
        }
    }
}
