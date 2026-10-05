using System;

namespace laba1
{
    public enum CatalogCommand
    {
        Exit = 0,
        AddGenericEquipment = 1,
        AddMonitor = 2,
        AddKeyboard = 3,
        ShowAll = 4,
        Remove = 5
    }

    public static class CatalogDemo
    {
        private const int FirstItemNumber = 1;
        public static void Run()
        {
            EquipmentCatalog catalog = new EquipmentCatalog();
            while (true)
            {
                CatalogCommand command = ReadCommand();
                if (command == CatalogCommand.Exit)
                {
                    break;
                }

                switch (command)
                {
                    case CatalogCommand.AddGenericEquipment:
                        catalog.Add(ReadGenericEquipment());
                        Console.WriteLine("Устройство добавлено в каталог.");
                        break;
                    case CatalogCommand.AddMonitor:
                        catalog.Add(ReadMonitor());
                        Console.WriteLine("Монитор добавлен в каталог.");
                        break;
                    case CatalogCommand.AddKeyboard:
                        catalog.Add(ReadKeyboard());
                        Console.WriteLine("Клавиатура добавлена в каталог.");
                        break;
                    case CatalogCommand.ShowAll:
                        ShowCatalog(catalog);
                        break;
                    case CatalogCommand.Remove:
                        RemoveItem(catalog);
                        break;
                }
            }
        }

        private static CatalogCommand ReadCommand()
        {
            Console.WriteLine();
            Console.WriteLine(" Каталог техники ");
            Console.WriteLine("{0}. Добавить устройство без уточнения категории", (int)CatalogCommand.AddGenericEquipment);
            Console.WriteLine("{0}. Добавить монитор", (int)CatalogCommand.AddMonitor);
            Console.WriteLine("{0}. Добавить клавиатуру", (int)CatalogCommand.AddKeyboard);
            Console.WriteLine("{0}. Вывести каталог", (int)CatalogCommand.ShowAll);
            Console.WriteLine("{0}. Удалить устройство по номеру", (int)CatalogCommand.Remove);
            Console.WriteLine("{0}. Выход в главное меню", (int)CatalogCommand.Exit);
            int choice = ConsoleInput.ReadIntInRange(
                "Выберите пункт: ",
                (int)CatalogCommand.Exit,
                (int)CatalogCommand.Remove);
            return (CatalogCommand)choice;
        }

        private static ComputerEquipment ReadGenericEquipment()
        {
            string brand = ConsoleInput.ReadNonEmptyLine("Торговая марка: ");
            decimal price = ConsoleInput.ReadDecimalAtLeast("Цена: ", ComputerEquipment.MinPrice);
            return new ComputerEquipment(brand, price);
        }

        private static Monitor ReadMonitor()
        {
            string brand = ConsoleInput.ReadNonEmptyLine("Торговая марка: ");
            decimal price = ConsoleInput.ReadDecimalAtLeast("Цена: ", ComputerEquipment.MinPrice);
            double diagonal = ConsoleInput.ReadDoubleGreaterThan(
                "Диагональ в дюймах: ", Monitor.MinDiagonalInches);
            int horizontalPixels = ConsoleInput.ReadIntInRange(
                "Пикселей по горизонтали: ", Monitor.MinPixels, int.MaxValue);
            int verticalPixels = ConsoleInput.ReadIntInRange(
                "Пикселей по вертикали: ", Monitor.MinPixels, int.MaxValue);
            return new Monitor(brand, price, diagonal, horizontalPixels, verticalPixels);
        }

        private static Keyboard ReadKeyboard()
        {
            string brand = ConsoleInput.ReadNonEmptyLine("Торговая марка: ");
            decimal price = ConsoleInput.ReadDecimalAtLeast("Цена: ", ComputerEquipment.MinPrice);
            KeyboardSwitchType switchType = ReadSwitchType();
            bool hasRgbBacklight = ConsoleInput.ReadYesNo("Есть RGB-подсветка (y/n): ");
            return new Keyboard(brand, price, switchType, hasRgbBacklight);
        }

        private static KeyboardSwitchType ReadSwitchType()
        {
            KeyboardSwitchType[] switchTypes =
                (KeyboardSwitchType[])Enum.GetValues(typeof(KeyboardSwitchType));
            Console.WriteLine("Тип переключателей:");
            for (int i = 0; i < switchTypes.Length; i++)
            {
                Console.WriteLine("  {0}. {1}", i + FirstItemNumber,
                    Keyboard.GetSwitchTypeName(switchTypes[i]));
            }

            int number = ConsoleInput.ReadIntInRange(
                "Выберите тип: ", FirstItemNumber, switchTypes.Length);
            return switchTypes[number - FirstItemNumber];
        }

        private static void ShowCatalog(EquipmentCatalog catalog)
        {
            if (catalog.IsEmpty)
            {
                Console.WriteLine("Каталог пуст.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(" Содержимое каталога ");
            int number = FirstItemNumber;
            foreach (ComputerEquipment item in catalog)
            {
                Console.WriteLine("{0}. {1}", number, item.GetDescription());
                number++;
            }

            Console.WriteLine("Всего устройств: {0}, суммарная стоимость: {1}{2}",
                catalog.Count,
                OutputFormat.CurrencySymbol,
                catalog.TotalPrice.ToString(OutputFormat.Price, OutputFormat.Culture));
        }

        private static void RemoveItem(EquipmentCatalog catalog)
        {
            if (catalog.IsEmpty)
            {
                Console.WriteLine("Каталог пуст, удалять нечего.");
                return;
            }

            int number = ConsoleInput.ReadIntInRange(
                string.Format("Номер устройства для удаления ({0}-{1}): ",
                    FirstItemNumber, catalog.Count),
                FirstItemNumber,
                catalog.Count);
            if (catalog.RemoveAt(number - FirstItemNumber))
            {
                Console.WriteLine("Устройство удалено из каталога.");
            }
            else
            {
                Console.WriteLine("Устройство с таким номером не найдено.");
            }
        }
    }
}
