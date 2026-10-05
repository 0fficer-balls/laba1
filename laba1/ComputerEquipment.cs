using System;

namespace laba1
{
    public class ComputerEquipment
    {
        public const string DefaultBrand = "Не указан";
        public const decimal MinPrice = 0m;
        public const string BaseCategoryName = "Компьютерная техника";
        private string _brand;
        private decimal _price;
        public ComputerEquipment()
            : this(DefaultBrand, MinPrice)
        {
        }

        public ComputerEquipment(string brand, decimal price)
        {
            Brand = brand;
            Price = price;
        }

        public string Brand
        {
            get { return _brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Торговая марка не может быть пустой.", nameof(value));
                }

                _brand = value.Trim();
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < MinPrice)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Цена не может быть отрицательной.");
                }

                _price = value;
            }
        }

        public virtual string CategoryName
        {
            get { return BaseCategoryName; }
        }

        public virtual string GetDescription()
        {
            return string.Format(
                OutputFormat.Culture,
                "{0}: марка {1}, цена {2}{3}",
                CategoryName,
                Brand,
                OutputFormat.CurrencySymbol,
                Price.ToString(OutputFormat.Price, OutputFormat.Culture));
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}
