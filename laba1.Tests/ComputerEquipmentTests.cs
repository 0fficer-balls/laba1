using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba1.Tests
{
    [TestClass]
    public class ComputerEquipmentTests
    {
        [TestMethod]
        public void Constructor_WithoutParameters_SetsDefaultValues()
        {
            ComputerEquipment equipment = new ComputerEquipment();
            Assert.AreEqual(ComputerEquipment.DefaultBrand, equipment.Brand);
            Assert.AreEqual(ComputerEquipment.MinPrice, equipment.Price);
        }

        [TestMethod]
        public void Constructor_WithParameters_SetsProperties()
        {
            ComputerEquipment equipment = new ComputerEquipment("Generic Corp", 45.50m);
            Assert.AreEqual("Generic Corp", equipment.Brand);
            Assert.AreEqual(45.50m, equipment.Price);
        }

        [TestMethod]
        public void Constructor_WithNegativePrice_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => new ComputerEquipment("Generic Corp", -0.01m));
        }

        [TestMethod]
        public void Brand_Setter_RemovesSurroundingSpaces()
        {
            ComputerEquipment equipment = new ComputerEquipment("   ASUS   ", 1m);
            Assert.AreEqual("ASUS", equipment.Brand);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("    ")]
        public void Brand_Setter_EmptyValue_ThrowsArgumentException(string brand)
        {
            ComputerEquipment equipment = new ComputerEquipment();
            Assert.ThrowsExactly<ArgumentException>(() => equipment.Brand = brand);
        }

        [TestMethod]
        public void Brand_Setter_RejectedValue_KeepsPreviousValue()
        {
            ComputerEquipment equipment = new ComputerEquipment("ASUS", 1m);
            try
            {
                equipment.Brand = string.Empty;
            }
            catch (ArgumentException)
            {
            }

            Assert.AreEqual("ASUS", equipment.Brand);
        }

        [TestMethod]
        [DataRow(-0.01)]
        [DataRow(-1000.0)]
        public void Price_Setter_NegativeValue_ThrowsArgumentOutOfRangeException(double price)
        {
            ComputerEquipment equipment = new ComputerEquipment();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => equipment.Price = (decimal)price);
        }

        [TestMethod]
        public void Price_Setter_Zero_IsAccepted()
        {
            ComputerEquipment equipment = new ComputerEquipment("ASUS", 100m);
            equipment.Price = ComputerEquipment.MinPrice;
            Assert.AreEqual(ComputerEquipment.MinPrice, equipment.Price);
        }

        [TestMethod]
        public void Price_Setter_RejectedValue_KeepsPreviousValue()
        {
            ComputerEquipment equipment = new ComputerEquipment("ASUS", 100m);
            try
            {
                equipment.Price = -5m;
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            Assert.AreEqual(100m, equipment.Price);
        }

        [TestMethod]
        public void CategoryName_ForBaseClass_ReturnsBaseCategory()
        {
            ComputerEquipment equipment = new ComputerEquipment();
            Assert.AreEqual(ComputerEquipment.BaseCategoryName, equipment.CategoryName);
        }

        [TestMethod]
        public void GetDescription_ReturnsCategoryBrandAndPrice()
        {
            ComputerEquipment equipment = new ComputerEquipment("Generic Corp", 45.5m);
            Assert.AreEqual(
                "Компьютерная техника: марка Generic Corp, цена $45.50",
                equipment.GetDescription());
        }

        [TestMethod]
        public void GetDescription_PrintsPriceWithTwoDecimalPlaces()
        {
            ComputerEquipment equipment = new ComputerEquipment("Generic Corp", 1200m);
            StringAssert.Contains(equipment.GetDescription(), "$1200.00");
        }

        [TestMethod]
        public void ToString_ReturnsSameTextAsGetDescription()
        {
            ComputerEquipment equipment = new ComputerEquipment("Generic Corp", 45.5m);
            Assert.AreEqual(equipment.GetDescription(), equipment.ToString());
        }
    }
}
