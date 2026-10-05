using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba1.Tests
{
    [TestClass]
    public class PolymorphismTests
    {
        private static List<ComputerEquipment> CreateMixedCollection()
        {
            return new List<ComputerEquipment>
            {
                new ComputerEquipment("Generic Corp", 45.50m),
                new Monitor("ASUS", 299.99m, 27.0, 2560, 1440),
                new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true)
            };
        }

        [TestMethod]
        public void CategoryName_ThroughBaseReference_UsesOverriddenImplementation()
        {
            List<ComputerEquipment> equipment = CreateMixedCollection();
            Assert.AreEqual(ComputerEquipment.BaseCategoryName, equipment[0].CategoryName);
            Assert.AreEqual(Monitor.MonitorCategoryName, equipment[1].CategoryName);
            Assert.AreEqual(Keyboard.KeyboardCategoryName, equipment[2].CategoryName);
        }

        [TestMethod]
        public void GetDescription_ThroughBaseReference_UsesOverriddenImplementation()
        {
            List<ComputerEquipment> equipment = CreateMixedCollection();
            StringAssert.StartsWith(equipment[0].GetDescription(), ComputerEquipment.BaseCategoryName);
            StringAssert.StartsWith(equipment[1].GetDescription(), Monitor.MonitorCategoryName);
            StringAssert.StartsWith(equipment[2].GetDescription(), Keyboard.KeyboardCategoryName);
        }

        [TestMethod]
        public void ToString_ThroughBaseReference_IsPolymorphic()
        {
            foreach (ComputerEquipment item in CreateMixedCollection())
            {
                Assert.AreEqual(item.GetDescription(), item.ToString(),
                    "ToString() должен возвращать результат виртуального GetDescription().");
            }
        }

        [TestMethod]
        public void DerivedDescription_ContainsCommonPartFromBaseClass()
        {
            ComputerEquipment monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            StringAssert.Contains(monitor.GetDescription(), "марка ASUS, цена $299.99");
        }

        [TestMethod]
        public void DerivedDescription_ContainsOwnCharacteristics()
        {
            ComputerEquipment monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            ComputerEquipment keyboard = new Keyboard("Logitech", 89.50m,
                KeyboardSwitchType.Optical, false);
            StringAssert.Contains(monitor.GetDescription(), "2560x1440");
            StringAssert.Contains(keyboard.GetDescription(), "оптические");
        }

        [TestMethod]
        public void BaseReference_KeepsActualObjectType()
        {
            List<ComputerEquipment> equipment = CreateMixedCollection();
            Assert.AreEqual(typeof(ComputerEquipment), equipment[0].GetType());
            Assert.AreEqual(typeof(Monitor), equipment[1].GetType());
            Assert.AreEqual(typeof(Keyboard), equipment[2].GetType());
        }

        [TestMethod]
        public void DownCast_GivesAccessToDerivedProperties()
        {
            List<ComputerEquipment> equipment = CreateMixedCollection();
            Monitor monitor = equipment[1] as Monitor;
            Keyboard keyboard = equipment[2] as Keyboard;
            Assert.IsNotNull(monitor, "Второй элемент должен приводиться к типу Monitor.");
            Assert.IsNotNull(keyboard, "Третий элемент должен приводиться к типу Keyboard.");
            Assert.AreEqual("2560x1440", monitor.Resolution);
            Assert.AreEqual(KeyboardSwitchType.Mechanical, keyboard.SwitchType);
        }

        [TestMethod]
        public void DownCast_OfBaseObjectToDerivedType_GivesNull()
        {
            ComputerEquipment equipment = new ComputerEquipment("Generic Corp", 45.50m);
            Assert.IsNull(equipment as Monitor);
            Assert.IsNull(equipment as Keyboard);
        }

        [TestMethod]
        public void SameReference_PointingToDifferentObjects_CallsDifferentImplementations()
        {
            ComputerEquipment reference = new ComputerEquipment("Generic Corp", 45.50m);
            string baseDescription = reference.GetDescription();
            reference = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            string monitorDescription = reference.GetDescription();
            reference = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
            string keyboardDescription = reference.GetDescription();
            CollectionAssert.AllItemsAreUnique(
                new List<string> { baseDescription, monitorDescription, keyboardDescription });
        }
    }
}
