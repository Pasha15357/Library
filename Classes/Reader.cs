using System;

namespace Библиотека
{
    // Класс читателя
    public class Reader
    {
        public int Id { get; set; }
        public int TicketNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }

        public Reader(int id, int ticketNumber, string name, string address, string phone, string passportNumber)
        {   
            Id = id;
            TicketNumber = ticketNumber;
            Name = name;
            Address = address;
            Phone = phone;
            PassportNumber = passportNumber;
        }

        // Метод по выводу читателя
        public void Show()
        {
            Console.WriteLine("====================Читатель====================\n" +
                                      $"Код читателя: {Id}\n" +
                                      $"Номер билета: {TicketNumber}\n" +
                                      $"Имя: {Name}\n" +
                                      $"Адрес: {Address}\n" +
                                      $"Номер телефона: {Phone}\n" +
                                      $"Номер паспорта: {PassportNumber}\n" +
                                      "================================================\n");
        }

        // Функция преобразования читателя в строку для записи в файл
        public override string ToString()
        {
            return $"{Id}|{TicketNumber}|{Name}|{Address}|{Phone}|{PassportNumber}";
        }

        // Функция чтения читателя из строки в файле
        public static Reader FromString(string str)
        {
            string[] fields = str.Split('|');
            return new Reader(int.Parse(fields[0]), int.Parse(fields[1]), fields[2], fields[3], fields[4], fields[5]);
        }
    }
}
