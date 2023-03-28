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
        
    }

}
