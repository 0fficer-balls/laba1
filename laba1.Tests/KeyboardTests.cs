using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba1.Tests
{
    [TestClass]
    public class KeyboardTests
    {
        [TestMethod]
        public void Constructor_WithoutParameters_SetsDefaultValues()
        {
            Keyboard keyboard = new Keyboard();
            Assert.AreEqual(ComputerEquipment.DefaultBrand, keyboard.Brand);
            Assert.AreEqual(ComputerEquipment.MinPrice, keyboard.Price);
            Assert.AreEqual(Keyboard.DefaultSwitchType, keyboard.SwitchType);
            Assert.AreEqual(Keyboard.DefaultHasRgbBacklight, keyboard.HasRgbBacklight);
        }

        [TestMethod]
        public void Constructor_WithParameters_SetsAllProperties()
        {
            Keyboard keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            Assert.AreEqual("Logitech", keyboard.Brand);
            Assert.AreEqual(89.50m, keyboard.Price);
            Assert.AreEqual(KeyboardSwitchType.Mechanical, keyboard.SwitchType);
            Assert.IsTrue(keyboard.HasRgbBacklight);
        }

        [TestMethod]
        public void Keyboard_IsDerivedFromComputerEquipment()
        {
            Keyboard keyboard = new Keyboard();
            Assert.IsInstanceOfType(keyboard, typeof(ComputerEquipment));
        }

        [TestMethod]
        public void SwitchType_Setter_AcceptsEveryDefinedValue()
        {
            Keyboard keyboard = new Keyboard();
            foreach (KeyboardSwitchType switchType in Enum.GetValues(typeof(KeyboardSwitchType)))
            {
                keyboard.SwitchType = switchType;
                Assert.AreEqual(switchType, keyboard.SwitchType);
            }
        }

        [TestMethod]
        public void SwitchType_Setter_ValueOutsideEnumeration_ThrowsArgumentOutOfRangeException()
        {
            Keyboard keyboard = new Keyboard();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(
                () => keyboard.SwitchType = (KeyboardSwitchType)100);
        }

        [TestMethod]
        public void HasRgbBacklight_CanBeSwitchedOff()
        {
            Keyboard keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            keyboard.HasRgbBacklight = false;
            Assert.IsFalse(keyboard.HasRgbBacklight);
        }

        [TestMethod]
        [DataRow(KeyboardSwitchType.Membrane, "мембранные")]
        [DataRow(KeyboardSwitchType.Mechanical, "механические")]
        [DataRow(KeyboardSwitchType.Optical, "оптические")]
        public void GetSwitchTypeName_ReturnsRussianName(KeyboardSwitchType switchType, string expected)
        {
            Assert.AreEqual(expected, Keyboard.GetSwitchTypeName(switchType));
        }

        [TestMethod]
        public void CategoryName_ReturnsKeyboardCategory()
        {
            Keyboard keyboard = new Keyboard();
            Assert.AreEqual(Keyboard.KeyboardCategoryName, keyboard.CategoryName);
        }

        [TestMethod]
        public void GetDescription_ReturnsBaseDescriptionExtendedWithKeyboardData()
        {
            Keyboard keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            Assert.AreEqual(
                "Клавиатура: марка Logitech, цена $89.50, "
                + "переключатели механические, RGB-подсветка есть",
                keyboard.GetDescription());
        }

        [TestMethod]
        [DataRow(true, "RGB-подсветка есть")]
        [DataRow(false, "RGB-подсветка нет")]
        public void GetDescription_ReportsBacklightState(bool hasRgbBacklight, string expected)
        {
            Keyboard keyboard = new Keyboard("Logitech", 89.50m,
                KeyboardSwitchType.Mechanical, hasRgbBacklight);
            StringAssert.Contains(keyboard.GetDescription(), expected);
        }
    }
}
