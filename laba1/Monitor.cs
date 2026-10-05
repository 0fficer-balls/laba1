using System;

namespace laba1
{
    public class Monitor : ComputerEquipment
    {
        public const double MinDiagonalInches = 0.0;
        public const int MinPixels = 1;
        public const double DefaultDiagonalInches = 24.0;
        public const int DefaultHorizontalPixels = 1920;
        public const int DefaultVerticalPixels = 1080;
        public const string MonitorCategoryName = "Монитор";
        private const string ResolutionSeparator = "x";
        private double _diagonalInches;
        private int _horizontalPixels;
        private int _verticalPixels;
        public Monitor()
            : this(DefaultBrand, MinPrice, DefaultDiagonalInches,
                   DefaultHorizontalPixels, DefaultVerticalPixels)
        {
        }

        public Monitor(string brand, decimal price, double diagonalInches,
                       int horizontalPixels, int verticalPixels)
            : base(brand, price)
        {
            DiagonalInches = diagonalInches;
            HorizontalPixels = horizontalPixels;
            VerticalPixels = verticalPixels;
        }

        public double DiagonalInches
        {
            get { return _diagonalInches; }
            set
            {
                if (value <= MinDiagonalInches)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Диагональ должна быть положительной.");
                }

                _diagonalInches = value;
            }
        }

        public int HorizontalPixels
        {
            get { return _horizontalPixels; }
            set
            {
                if (value < MinPixels)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Число пикселей по горизонтали должно быть положительным.");
                }

                _horizontalPixels = value;
            }
        }

        public int VerticalPixels
        {
            get { return _verticalPixels; }
            set
            {
                if (value < MinPixels)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Число пикселей по вертикали должно быть положительным.");
                }

                _verticalPixels = value;
            }
        }

        public string Resolution
        {
            get
            {
                return _horizontalPixels.ToString(OutputFormat.Culture)
                       + ResolutionSeparator
                       + _verticalPixels.ToString(OutputFormat.Culture);
            }
        }

        public double PixelsPerInch
        {
            get
            {
                double diagonalInPixels = Math.Sqrt(
                    (double)_horizontalPixels * _horizontalPixels +
                    (double)_verticalPixels * _verticalPixels);
                return diagonalInPixels / _diagonalInches;
            }
        }

        public override string CategoryName
        {
            get { return MonitorCategoryName; }
        }

        public override string GetDescription()
        {
            return string.Format(
                OutputFormat.Culture,
                "{0}, диагональ {1}\", разрешение {2}, плотность {3} PPI",
                base.GetDescription(),
                _diagonalInches.ToString(OutputFormat.SingleDecimal, OutputFormat.Culture),
                Resolution,
                PixelsPerInch.ToString(OutputFormat.SingleDecimal, OutputFormat.Culture));
        }
    }
}
