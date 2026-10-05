using System;
using System.IO;
using System.Text;

namespace laba1
{
    public enum MainMenuCommand
    {
        Exit = 0,
        BasicUsage = 1,
        Polymorphism = 2,
        Catalog = 3
    }

    internal class Program
    {
        private const int CyrillicCodePage = 1251;
        private static void Main()
        {
            ConfigureConsole();
            try
            {
                RunMainMenu();
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine();
                Console.WriteLine("Ввод завершён, работа программы окончена.");
            }
        }

        private static void RunMainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("  Компьютерная техника: демонстрация классов  ");
                Console.WriteLine("{0}. Создание объектов и работа со свойствами", (int)MainMenuCommand.BasicUsage);
                Console.WriteLine("{0}. Коллекция ссылок базового класса и полиморфизм", (int)MainMenuCommand.Polymorphism);
                Console.WriteLine("{0}. Каталог техники (ввод данных с клавиатуры)", (int)MainMenuCommand.Catalog);
                Console.WriteLine("{0}. Выход", (int)MainMenuCommand.Exit);
                int choice = ConsoleInput.ReadIntInRange(
                    "Выберите пункт: ",
                    (int)MainMenuCommand.Exit,
                    (int)MainMenuCommand.Catalog);
                MainMenuCommand command = (MainMenuCommand)choice;
                if (command == MainMenuCommand.Exit)
                {
                    return;
                }

                Console.WriteLine();
                switch (command)
                {
                    case MainMenuCommand.BasicUsage:
                        BasicUsageDemo.Run();
                        break;
                    case MainMenuCommand.Polymorphism:
                        PolymorphismDemo.Run();
                        break;
                    case MainMenuCommand.Catalog:
                        CatalogDemo.Run();
                        break;
                }
            }
        }

        private static void ConfigureConsole()
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(CyrillicCodePage);
                Console.OutputEncoding = encoding;
                Console.InputEncoding = encoding;
            }
            catch (IOException)
            {
            }
        }
    }
}
