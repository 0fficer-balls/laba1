using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba1.Tests
{
    [TestClass]
    public class MonitorTests
    {
        [TestMethod]
        public void Constructor_WithoutParameters_SetsDefaultValues()
        {
            Monitor monitor = new Monitor();
            Assert.AreEqual(ComputerEquipment.DefaultBrand, monitor.Brand);
            Assert.AreEqual(ComputerEquipment.MinPrice, monitor.Price);
            Assert.AreEqual(Monitor.DefaultDiagonalInches, monitor.DiagonalInches);
            Assert.AreEqual(Monitor.DefaultHorizontalPixels, monitor.HorizontalPixels);
            Assert.AreEqual(Monitor.DefaultVerticalPixels, monitor.VerticalPixels);
        }

        [TestMethod]
        public void Constructor_WithParameters_SetsAllProperties()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Assert.AreEqual("ASUS", monitor.Brand);
            Assert.AreEqual(299.99m, monitor.Price);
            Assert.AreEqual(27.0, monitor.DiagonalInches);
            Assert.AreEqual(2560, monitor.HorizontalPixels);
            Assert.AreEqual(1440, monitor.VerticalPixels);
        }

        [TestMethod]
        public void Monitor_IsDerivedFromComputerEquipment()
        {
            Monitor monitor = new Monitor();
            Assert.IsInstanceOfType(monitor, typeof(ComputerEquipment));
        }

        [TestMethod]
        public void Resolution_CombinesPixelCountsWithSeparator()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Assert.AreEqual("2560x1440", monitor.Resolution);
        }

        [TestMethod]
        public void Resolution_ChangesAfterPixelCountIsUpdated()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            monitor.HorizontalPixels = 3840;
            monitor.VerticalPixels = 2160;
            Assert.AreEqual("3840x2160", monitor.Resolution);
        }

        [TestMethod]
        public void PixelsPerInch_IsComputedFromResolutionAndDiagonal()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Assert.AreEqual(108.7855, monitor.PixelsPerInch, 0.001);
        }

        [TestMethod]
        public void PixelsPerInch_GrowsWhenDiagonalDecreases()
        {
            Monitor large = new Monitor("ASUS", 299.99m, 32.0, 2560, 1440);
            Monitor small = new Monitor("ASUS", 299.99m, 24.0, 2560, 1440);
            Assert.IsTrue(small.PixelsPerInch > large.PixelsPerInch,
                "При одинаковом разрешении меньшая диагональ должна давать большую плотность.");
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-27.0)]
        public void DiagonalInches_NonPositiveValue_ThrowsArgumentOutOfRangeException(double diagonal)
        {
            Monitor monitor = new Monitor();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => monitor.DiagonalInches = diagonal);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1920)]
        public void HorizontalPixels_NonPositiveValue_ThrowsArgumentOutOfRangeException(int pixels)
        {
            Monitor monitor = new Monitor();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => monitor.HorizontalPixels = pixels);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1080)]
        public void VerticalPixels_NonPositiveValue_ThrowsArgumentOutOfRangeException(int pixels)
        {
            Monitor monitor = new Monitor();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => monitor.VerticalPixels = pixels);
        }

        [TestMethod]
        public void CategoryName_ReturnsMonitorCategory()
        {
            Monitor monitor = new Monitor();
            Assert.AreEqual(Monitor.MonitorCategoryName, monitor.CategoryName);
        }

        [TestMethod]
        public void GetDescription_ReturnsBaseDescriptionExtendedWithMonitorData()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            Assert.AreEqual(
                "Монитор: марка ASUS, цена $299.99, диагональ 27\", "
                + "разрешение 2560x1440, плотность 108.8 PPI",
                monitor.GetDescription());
        }

        [TestMethod]
        public void GetDescription_StartsWithMonitorCategory()
        {
            Monitor monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            StringAssert.StartsWith(monitor.GetDescription(), Monitor.MonitorCategoryName);
        }
    }
}
