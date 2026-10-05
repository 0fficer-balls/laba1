using System;

namespace laba1
{
    public static class PolymorphismDemo
    {
        public static void Run()
        {
            ComputerEquipment genericDevice = new ComputerEquipment("Generic Corp", 45.50m);
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Keyboard keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            ComputerEquipment[] equipment = { genericDevice, monitor, keyboard };
            Console.WriteLine(" Полиморфный вызов виртуальных членов ");
            Console.WriteLine("Все элементы объявлены как ComputerEquipment,");
            Console.WriteLine("но каждый выполняет реализацию своего класса.");
            Console.WriteLine();
            foreach (ComputerEquipment item in equipment)
            {
                Console.WriteLine("Фактический тип: {0}", item.GetType().Name);
                Console.WriteLine("  CategoryName   : {0}", item.CategoryName);
                Console.WriteLine("  GetDescription : {0}", item.GetDescription());
                Console.WriteLine();
            }

            Console.WriteLine(" Доступ к характеристикам производных классов ");
            foreach (ComputerEquipment item in equipment)
            {
                Monitor asMonitor = item as Monitor;
                Keyboard asKeyboard = item as Keyboard;
                if (asMonitor != null)
                {
                    Console.WriteLine("{0}: разрешение {1}", asMonitor.Brand, asMonitor.Resolution);
                }
                else if (asKeyboard != null)
                {
                    Console.WriteLine("{0}: переключатели {1}", asKeyboard.Brand,
                        Keyboard.GetSwitchTypeName(asKeyboard.SwitchType));
                }
                else
                {
                    Console.WriteLine("{0}: дополнительных характеристик нет", item.Brand);
                }
            }

            Console.WriteLine();
            Console.WriteLine(" Одна ссылка на объекты разных классов ");
            ComputerEquipment reference = genericDevice;
            Console.WriteLine("ссылка -> ComputerEquipment: {0}", reference.GetDescription());
            reference = monitor;
            Console.WriteLine("ссылка -> Monitor          : {0}", reference.GetDescription());
            reference = keyboard;
            Console.WriteLine("ссылка -> Keyboard         : {0}", reference.GetDescription());
        }
    }
}
