using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Библиотека
{
    internal class BookInterface //класс интерфейса книг, создающий меню работы с книгами
    {
        public static void BookMenu() //метод главного меню по работе с книгами
        {
            List<Book> books = new List<Book>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("books.xml");
            XmlElement xRoot = xDoc.DocumentElement;
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
                    ShowBooks(books); //при вводе 1 демонстрируются все книги
                    break;
                case '2':
                    AddBook(books); //при вводе 2 осуществляется переход в меню с добавлением книги
                    break;
                /*case '3':
                    DeleteBook(ref books); //при вводе 3 осуществляется переход в меню с удалением книги
                    break;*/
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

        private static void ShowBooks(List<Book> books) //метод по выводу всех книг
        {

            Console.Clear(); //очищаем полностью консоль
            if (books.Count() != 0) //если книги есть
            {
                foreach (Book b in books)
                    b.Show();
            }
            else //если число книг равно 0
            {
                Console.WriteLine("Книг нет");
            }
            Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
            Console.ReadKey();
            BookMenu(); //возвращаемся в меню работы с книгами
        }

        private static void AddBook(List<Book> books) //метод по добавлению книги
        {
            Console.Clear();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("books.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент book
            XmlElement bookElem = xDoc.CreateElement("book");
            // создаем атрибут name
            XmlAttribute idAttr = xDoc.CreateAttribute("id");
            // создаем элементы company и age
            XmlElement titleElem = xDoc.CreateElement("title");
            XmlElement authorElem = xDoc.CreateElement("author");
            XmlElement priceElem = xDoc.CreateElement("price");
            XmlElement genreElem = xDoc.CreateElement("genre");
            XmlElement yearElem = xDoc.CreateElement("year");
            // создаем текстовые значения для элементов и атрибута
            try
            {
                XmlText idText = xDoc.CreateTextNode(books.Count().ToString());
                Console.WriteLine("Введите название книги: ");
                XmlText titleText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите автора книги: ");
                XmlText authorText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите цену книги: ");
                XmlText priceText = xDoc.CreateTextNode(int.Parse(Console.ReadLine()).ToString());
                Console.WriteLine("Введите жанр книги: ");
                XmlText genreText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите год издания книги: ");
                XmlText yearText = xDoc.CreateTextNode(int.Parse(Console.ReadLine()).ToString());
                //добавляем узлы
                idAttr.AppendChild(idText);
                titleElem.AppendChild(titleText);
                authorElem.AppendChild(authorText);
                priceElem.AppendChild(priceText);
                genreElem.AppendChild(genreText);
                yearElem.AppendChild(yearText);
                bookElem.Attributes.Append(idAttr);
                bookElem.AppendChild(titleElem);
                bookElem.AppendChild(authorElem);
                bookElem.AppendChild(priceElem);
                bookElem.AppendChild(genreElem);
                bookElem.AppendChild(yearElem);
                xRoot.AppendChild(bookElem);
                xDoc.Save("books.xml");
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверный формат");
                Console.WriteLine("Нажмите любую клавишу, чтобы ввести заново");
                Console.ReadKey();
                BookMenu();
            }
            BookMenu(); //возвращаемся в меню работы с книгами
        }

        /*static void DeleteBook(ref ICollection<Book> books) //метод по удалению книги
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
        }*/
    }
}
