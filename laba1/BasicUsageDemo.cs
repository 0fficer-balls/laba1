using System;

namespace laba1
{
    public static class BasicUsageDemo
    {
        public static void Run()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Keyboard keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            Console.WriteLine(" Исходные характеристики объектов ");
            Console.WriteLine(monitor);
            Console.WriteLine(keyboard);
            Console.WriteLine();
            Console.WriteLine(" Изменение характеристик ");
            monitor.Price = 259.99m;
            monitor.HorizontalPixels = 3840;
            monitor.VerticalPixels = 2160;
            Console.WriteLine("Монитору снижена цена и увеличено разрешение.");
            keyboard.Brand = "Razer";
            keyboard.HasRgbBacklight = false;
            Console.WriteLine("Клавиатуре изменена марка и отключена подсветка.");
            Console.WriteLine();
            Console.WriteLine(" Обновлённые характеристики объектов ");
            Console.WriteLine(monitor);
            Console.WriteLine(keyboard);
            Console.WriteLine();
            Console.WriteLine("Вычисляемые свойства монитора:");
            Console.WriteLine("  разрешение — {0}", monitor.Resolution);
            Console.WriteLine("  плотность пикселей — {0} PPI",
                monitor.PixelsPerInch.ToString(OutputFormat.SingleDecimal, OutputFormat.Culture));
            Console.WriteLine();
            Console.WriteLine(" Проверка недопустимых значений ");
            TryAssignInvalidPrice(monitor, -100m);
            TryAssignInvalidDiagonal(monitor, 0.0);
            TryAssignInvalidBrand(keyboard, "   ");
            Console.WriteLine("Состояние объектов не изменилось:");
            Console.WriteLine(monitor);
            Console.WriteLine(keyboard);
        }

        private static void TryAssignInvalidPrice(ComputerEquipment equipment, decimal price)
        {
            try
            {
                equipment.Price = price;
                Console.WriteLine("Цена {0} принята — проверка не сработала!", price);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine("Цена {0} отклонена: {1}", price, GetShortMessage(exception));
            }
        }

        private static void TryAssignInvalidDiagonal(Monitor monitor, double diagonalInches)
        {
            try
            {
                monitor.DiagonalInches = diagonalInches;
                Console.WriteLine("Диагональ {0} принята — проверка не сработала!", diagonalInches);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine("Диагональ {0} отклонена: {1}", diagonalInches, GetShortMessage(exception));
            }
        }

        private static void TryAssignInvalidBrand(ComputerEquipment equipment, string brand)
        {
            try
            {
                equipment.Brand = brand;
                Console.WriteLine("Пустая марка принята — проверка не сработала!");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Пустая марка отклонена: {0}", GetShortMessage(exception));
            }
        }

        private static string GetShortMessage(ArgumentException exception)
        {
            int lineBreak = exception.Message.IndexOf(Environment.NewLine, StringComparison.Ordinal);
            return lineBreak < 0 ? exception.Message : exception.Message.Substring(0, lineBreak);
        }
    }
}
