using System;

namespace Библиотека
{
    internal class Main //класс, создающий главное меню
    {
        public static void MainMenu() //метод главного меню
        {
            Console.Clear(); //очищаем полностью консоль
            Console.WriteLine("                      ===============================\n" +
                              "                      |         1. Читатели         |\n" +
                              "                      ===============================\n" +
                              "                      |           2. Книги          |\n" +
                              "                      ===============================\n" +
                              "                      |        3. Выдачи книг       |\n" +
                              "                      ===============================\n" +
                              "                      |    4. Выход из программы    |\n" +
                              "                      ===============================");
            Console.WriteLine();
            Console.Write("Введите код операции:  ");
            char Code = Console.ReadKey(true).KeyChar; //считываем введенный код и переходим по соответствующему меню
            switch (Code) 
            {
                case '1':
                    ReaderInterface.ReaderMenu(); //при вводе 1 переходим в меню с читателями
                    break;
                case '2':
                    BookInterface.BookMenu();  //при вводе 2 переходим в меню с книгами
                    break;
                case '3':
                    LendingInterface.LendingMenu(); //при вводе 3 переходим в меню с выдачей книг
                    break;
                case '4':
                    Environment.Exit(0); //при вводе 4 выходим из приложения
                    break;
                default:
                    Console.WriteLine("Неверный код операции"); //при вводе любого другого числа дается попытка ввода числа еще раз
                    Console.ReadKey();
                    MainMenu();
                    break;
            }
        }
    }
}
