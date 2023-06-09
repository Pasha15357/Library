﻿using System;
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
                case '2':
                    AddReader(readers); //при вводе 2 осуществляется переход в меню с добавлением читателя
                    break;
                case '3':
                    DeleteReader(readers); //при вводе 3 осуществляется переход в меню с удалением читателя
                    break;
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

        static void AddReader(List<Reader> readers) //метод по добавлению читателя
        {
            Console.Clear();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("readers.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент book
            XmlElement readerElem = xDoc.CreateElement("reader");
            // создаем атрибут name
            XmlAttribute idAttr = xDoc.CreateAttribute("id");
            // создаем элементы company и age
            XmlElement ticketNumberElem = xDoc.CreateElement("ticket_number");
            XmlElement nameElem = xDoc.CreateElement("name");
            XmlElement addressElem = xDoc.CreateElement("address");
            XmlElement phoneElem = xDoc.CreateElement("phone");
            XmlElement passportNumberElem = xDoc.CreateElement("passport_number");
            // создаем текстовые значения для элементов и атрибута
            try
            {
                XmlText idText = xDoc.CreateTextNode(readers.Count().ToString());
                Console.WriteLine("Введите номер билета читателя: ");
                XmlText ticketNumberText = xDoc.CreateTextNode(int.Parse(Console.ReadLine()).ToString());
                Console.WriteLine("Введите имя читателя: ");
                XmlText nameText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите адрес читателя: ");
                XmlText addressText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите номер телефона читателя: ");
                XmlText phoneText = xDoc.CreateTextNode(Console.ReadLine());
                Console.WriteLine("Введите номер паспорта читателя: ");
                XmlText passportNumberText = xDoc.CreateTextNode(Console.ReadLine());
                //добавляем узлы
                idAttr.AppendChild(idText);
                ticketNumberElem.AppendChild(ticketNumberText);
                nameElem.AppendChild(nameText);
                addressElem.AppendChild(addressText);
                phoneElem.AppendChild(phoneText);
                passportNumberElem.AppendChild(passportNumberText);
                readerElem.Attributes.Append(idAttr);
                readerElem.AppendChild(ticketNumberElem);
                readerElem.AppendChild(nameElem);
                readerElem.AppendChild(addressElem);
                readerElem.AppendChild(phoneElem);
                readerElem.AppendChild(passportNumberElem);
                xRoot.AppendChild(readerElem);
                xDoc.Save("readers.xml");
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверный формат");
                Console.WriteLine("Нажмите любую клавишу, чтобы ввести заново");
                Console.ReadKey();
                ReaderMenu();
            }
            ReaderMenu(); //возвращаемся в меню работы с книгами
        }

        static void DeleteReader(List<Reader> readers) //метод по удалению читателя
        {
            Console.Clear(); //очищаем полностью консоль
            if (readers.Count == 0) //если число книг равно 0
            {
                Console.WriteLine("Выдач книг нет.");
                Console.Write("Нажмите любую клавишу, чтобы вернуться назад: ");
                Console.ReadKey();
                ReaderMenu(); //возвращаемся в меню работы с выдачей книг
            }
            else
            {
                foreach (Reader reader in readers) //выводим каждую выдачу книги в колллекции
                {
                    reader.Show(); //метод вывода одной выдачи книги
                }
                try
                {
                    Console.WriteLine("Введите код записи, которую хотите удалить: ");
                    int reader_code = int.Parse(Console.ReadLine());
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("readers.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlElement xnode in xRoot)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("id");
                        if (attr.Value == reader_code.ToString())
                        {
                            xRoot.RemoveChild(xnode);
                        }
                    }

                    xDoc.Save("readers.xml");
                    Console.Clear(); //очищаем полностью консоль
                }
                catch
                {
                    Console.WriteLine("Ошибка! Такой книги не существует!");
                    Console.WriteLine("Нажмите любую клавишу, чтобы выйти в меню");
                    Console.ReadKey();
                    ReaderMenu();
                }
                ReaderMenu(); //возвращаемся в меню работы с книгами
            }
        }
    }
}
