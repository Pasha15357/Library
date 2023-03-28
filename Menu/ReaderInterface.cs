using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Библиотека
{
    internal class ReaderInterface //класс интерфейса читателей, создающий меню работы с читателями
    {
        public static void ReaderMenu() //метод меню по работе с читателями
        {
            List<Reader> readers = new List<Reader>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("readers.xml");
            XmlElement xRoot = xDoc.DocumentElement;
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
                              "                      |     1. Список читателей     |\n" +
                              "                      ===============================\n" +
                              "                      |    2. Добавить читателя     |\n" +
                              "                      ===============================\n" +
                              "                      |     3. Удалить читателя     |\n" +
                              "                      ===============================\n" +
                              "                      |   4. Выход в главное меню   |\n" +
                              "                      ===============================");
            Console.WriteLine();
            Console.Write("Введите код операции:  ");
            char Code = Console.ReadKey(true).KeyChar; //считываем введенный код и переходим по соответствующему меню
            
            switch (Code)
            {
                case '1':
                    ShowReaders(readers); //при вводе 1 демонстрируются все читатели
                    break; 
                /*case '2':
                    AddReader(ref readers, ref id); //при вводе 2 осуществляется переход в меню с добавлением читателя
                    break;
                case '3':
                    DeleteReader(ref readers); //при вводе 3 осуществляется переход в меню с удалением читателя
                    break;*/
                case '4':
                    Main.MainMenu(); //при вводе 4 осуществляется переход в главное меню приложения
                    break;
                default:
                    Console.WriteLine("Неверный код операции"); //при вводе любого другого числа дается попытка ввода числа еще раз
                    Console.ReadKey();
                    ReaderMenu();
                    break;
            }
        }

        internal static void ShowReaders(List<Reader> readers) //метод по выводу всех читателей
        {            
            Console.Clear(); //очищаем полностью консоль
            if (readers.Count() != 0) //если книги есть
            {
                foreach (Reader reader in readers) //выводим каждого читателя колллекции
                {
                    reader.Show(); //метод по выводу одного читателя
                }
            }
            else //если число читателей равно 0
            {
                Console.WriteLine("Читателей нет");
            }
            Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
            Console.ReadKey();
            ReaderMenu(); //возвращаемся в меню работы с читателями
        }

        /*static void AddReader(ref ICollection<Reader> readers, ref int id) //метод по добавлению читателя
        {
            using (StreamReader reading = new("readers.txt")) //присваеваем потоку чтения файл с читателями
            {
                while (!reading.EndOfStream) //пока не достигнут конец файла
                {
                    readers.Add(Reader.FromString(reading.ReadLine())); //применяем метод из Reader.cs по чтению строки и добавляем читателя в колллекцию
                    if (readers == null) //если книг нет
                    {
                        id = 0; //первый ID=0
                    }
                    else
                    {
                        id = readers.Last().Id + 1; //иначе к последнему добавляем 1, чтобы следующий читатель был с ID на 1 больше
                    } 
                }
            }
            Console.Clear(); //очищаем полностью консоль
            try
            {
                Console.WriteLine("Введите номер билета");
                int TicketNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите имя читателя");
                string Name = Console.ReadLine();
                Console.WriteLine("Введите адрес");
                string Address = Console.ReadLine();
                Console.WriteLine("Введите номер телефона");
                string Phone = Console.ReadLine();
                Console.WriteLine("Введите номер паспорта");
                string PassportNumber = Console.ReadLine();
                Reader reader = new Reader(id, TicketNumber, Name, Address, Phone, PassportNumber); //применяем конструктор из Reader.cs и записываем все введенные данные в нового читателя
                readers.Add(reader); //добавляем в коллекцию нового читателя
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверный формат");
                Console.WriteLine("Нажмите любую клавишу, чтобы ввести заново");
                Console.ReadKey();
                ReaderMenu();
            }

            using (StreamWriter writer = new("readers.txt", false)) //создаем новый поток записи, который запишет нового читателя в файл
            {
                foreach (Reader _reader in readers)
                {
                    writer.WriteLine(_reader.ToString()); //применяем метод из Reader.cs по записи метода файл к каждому читателю
                }
            }
            ReaderMenu(); //возвращаемся в меню работы с читателями
        }

        static void DeleteReader(ref ICollection<Reader> readers) //метод по удалению читателя
        {
            using (StreamReader reader = new("readers.txt")) //присваеваем потоку чтения файл с читателями
            {
                while (!reader.EndOfStream) //пока не достигнут конец файла
                {
                    readers.Add(Reader.FromString(reader.ReadLine())); //применяем метод из Reader.cs по чтению строки и добавляем читателя в колллекцию
                }
            }
            Console.Clear(); //очищаем полностью консоль
            foreach (Reader reader in readers) //выводим каждого читателя колллекции
            {
                reader.Show(); //метод по выводу одного читателя
            }
            if (readers.Count == 0) //если число читателей равно 0
            {
                Console.WriteLine("Читателей нет.");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                ReaderMenu(); //возвращаемся в меню работы с читателями
            }
            else
            {
                try
                {
                    Console.WriteLine("Введите код записи, которую хотите удалить: ");
                    int id = int.Parse(Console.ReadLine());
                    var temp = readers.Where(d => d.Id == id).First(); //проходимся по всей коллекции, пока не встретим читателя с введенным ID и записываем его в переменную
                    readers.Remove(temp); //удаляем из коллекции
                    using (StreamWriter writer = new("readers.txt")) //создаем новый поток записи, который удалит читателя из файла
                    {
                        foreach (Reader _reader in readers)
                        {
                            writer.WriteLine(_reader.ToString()); //записываем в файл пустое значение вместо удаленного читателя
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Такой книги не существует!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы выйти в меню");
                    Console.ReadKey();
                    ReaderMenu();
                }
                ReaderMenu(); //возвращаемся в меню работы с читателями
            }
        }*/
    }
}
