using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Библиотека
{
    internal class LendingInterface //класс интерфейса выдачи книг, создающий меню работы с выдачами книг
    {
        public static void LendingMenu() //метод меню по работе с выдачами книг

        {
            List<Lending> lendings = new List<Lending>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("lendings.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Lending lending = new Lending();
                XmlNode attr = xnode.Attributes.GetNamedItem("id");

                if (attr != null)
                    lending.Id = Int32.Parse(attr.Value);

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "title")
                        lending.Title = childnode.InnerText;

                    if (childnode.Name == "ticket_number")
                        lending.TicketNumber = Int32.Parse(childnode.InnerText);

                    if (childnode.Name == "issue_date")
                        lending.IssueDate = childnode.InnerText;

                    if (childnode.Name == "usage_period")
                        lending.UsagePeriod = childnode.InnerText;

                    if (childnode.Name == "librarian_name")
                        lending.LibrarianName = childnode.InnerText;
                }
                lendings.Add(lending);
            }

            List<Book> books = new List<Book>();
            XmlDocument xDoc1 = new XmlDocument();
            xDoc.Load("books.xml");
            XmlElement xRoot1 = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Book book = new Book();
                XmlNode attr = xnode.Attributes.GetNamedItem("id");

                if (attr != null)
                    book.Id = Int32.Parse(attr.Value);

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "title")
                        book.Author = childnode.InnerText;

                    if (childnode.Name == "author")
                        book.Author = childnode.InnerText;

                    if (childnode.Name == "price")
                        book.Price = Int32.Parse(childnode.InnerText);

                    if (childnode.Name == "genre")
                        book.Genre = childnode.InnerText;

                    if (childnode.Name == "year")
                        book.Year = Int32.Parse(childnode.InnerText);
                }
                books.Add(book);
            }

            List<Reader> readers = new List<Reader>();
            XmlDocument xDoc2 = new XmlDocument();
            xDoc.Load("readers.xml");
            XmlElement xRoot2 = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Reader reader = new Reader();
                XmlNode attr = xnode.Attributes.GetNamedItem("id");
                if (attr != null)
                    reader.Id = int.Parse(attr.Value);

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "ticket_number")
                        reader.TicketNumber = Int32.Parse(childnode.InnerText);

                    if (childnode.Name == "name")
                        reader.Name = childnode.InnerText;

                    if (childnode.Name == "address")
                        reader.Address = childnode.InnerText;

                    if (childnode.Name == "phone")
                        reader.Phone = childnode.InnerText;

                    if (childnode.Name == "passport_number")
                        reader.PassportNumber = childnode.InnerText;
                }
                readers.Add(reader);
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
                /*case '2':
                    AddLending(readers, books, ref lendings, ref id); //при вводе 2 осуществляется переход в меню с добавлением выдачи книги
                    break;
                case '3':
                    DeleteLending(ref lendings); //при вводе 3 осуществляется переход в меню с удалением выдачи книги
                    break;*/
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

        /*static void AddLending(ICollection<Reader> readers, ICollection<Book> books, ref ICollection<Lending> lendings, ref int id) //метод по добавлению выдачи книги
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
            
        }*/
    }
}
