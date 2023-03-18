using System;

namespace Библиотека
{
    // Класс выдачи книги
    public class Lending
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TicketNumber { get; set; }
        public string IssueDate { get; set; }
        public string UsagePeriod { get; set; }
        public string LibrarianName { get; set; }

        public Lending(int id, string title, int ticketNumber, string issueDate, string usagePeriod, string librarianName)
        {
            Id = id;
            Title = title;
            TicketNumber = ticketNumber;
            IssueDate = issueDate;
            UsagePeriod = usagePeriod;
            LibrarianName = librarianName;
        }

        public void Show()
        {
            Console.WriteLine("===================Выдача книг===================\n" +
                                      $"Код выдачи книги: {Id}\n" +
                                      $"Название книги: {Title}\n" +
                                      $"Номер билета: {TicketNumber}\n" +
                                      $"Дата выдачи: {IssueDate}\n" +
                                      $"Период использования: {UsagePeriod}\n" +
                                      $"Имя библиотекаря: {LibrarianName}\n" +
                                      "=================================================\n");
        }
        // Функция преобразования выдачи книги в строку для записи в файл
        public override string ToString()
        {
            return $"{Id}|{Title}|{TicketNumber}|{IssueDate}|{UsagePeriod}|{LibrarianName}";
        }

        // Функция чтения выдачи книги из строки в файле
        public static Lending FromString(string str)
        {
            string[] fields = str.Split('|');
            return new Lending(int.Parse(fields[0]), fields[1], int.Parse(fields[2]), fields[3], fields[4], fields[5]);
        }
    }

}
