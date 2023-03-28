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

        
    }
}
