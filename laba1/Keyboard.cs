using System;

namespace laba1
{
    public class Keyboard : ComputerEquipment
    {
        public const KeyboardSwitchType DefaultSwitchType = KeyboardSwitchType.Membrane;
        public const bool DefaultHasRgbBacklight = false;
        public const string KeyboardCategoryName = "Клавиатура";
        private const string BacklightPresentText = "есть";
        private const string BacklightAbsentText = "нет";
        private KeyboardSwitchType _switchType;
        public Keyboard()
            : this(DefaultBrand, MinPrice, DefaultSwitchType, DefaultHasRgbBacklight)
        {
        }

        public Keyboard(string brand, decimal price, KeyboardSwitchType switchType, bool hasRgbBacklight)
            : base(brand, price)
        {
            SwitchType = switchType;
            HasRgbBacklight = hasRgbBacklight;
        }

        public KeyboardSwitchType SwitchType
        {
            get { return _switchType; }
            set
            {
                if (!Enum.IsDefined(typeof(KeyboardSwitchType), value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Неизвестный тип переключателей клавиатуры.");
                }

                _switchType = value;
            }
        }

        public bool HasRgbBacklight { get; set; }
        public override string CategoryName
        {
            get { return KeyboardCategoryName; }
        }

        public override string GetDescription()
        {
            return string.Format(
                OutputFormat.Culture,
                "{0}, переключатели {1}, RGB-подсветка {2}",
                base.GetDescription(),
                GetSwitchTypeName(SwitchType),
                HasRgbBacklight ? BacklightPresentText : BacklightAbsentText);
        }

        public static string GetSwitchTypeName(KeyboardSwitchType switchType)
        {
            switch (switchType)
            {
                case KeyboardSwitchType.Membrane:
                    return "мембранные";
                case KeyboardSwitchType.Mechanical:
                    return "механические";
                case KeyboardSwitchType.Optical:
                    return "оптические";
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(switchType), switchType, "Неизвестный тип переключателей клавиатуры.");
            }
        }
    }
}
