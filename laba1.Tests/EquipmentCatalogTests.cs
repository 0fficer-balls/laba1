using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba1.Tests
{
    [TestClass]
    public class EquipmentCatalogTests
    {
        private EquipmentCatalog _catalog;
        private ComputerEquipment _genericDevice;
        private Monitor _monitor;
        private Keyboard _keyboard;
        [TestInitialize]
        public void SetUp()
        {
            _catalog = new EquipmentCatalog();
            _genericDevice = new ComputerEquipment("Generic Corp", 45.50m);
            _monitor = new Monitor("ASUS", 299.99m, 27.0, 2560, 1440);
            _keyboard = new Keyboard("Logitech", 89.50m, KeyboardSwitchType.Mechanical, true);
        }

        private void AddAllThreeDevices()
        {
            _catalog.Add(_genericDevice);
            _catalog.Add(_monitor);
            _catalog.Add(_keyboard);
        }

        [TestMethod]
        public void NewCatalog_IsEmpty()
        {
            Assert.IsTrue(_catalog.IsEmpty);
            Assert.AreEqual(0, _catalog.Count);
            Assert.AreEqual(0m, _catalog.TotalPrice);
        }

        [TestMethod]
        public void Add_IncreasesCountAndClearsEmptyFlag()
        {
            _catalog.Add(_monitor);
            Assert.AreEqual(1, _catalog.Count);
            Assert.IsFalse(_catalog.IsEmpty);
        }

        [TestMethod]
        public void Add_Null_ThrowsArgumentNullException()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => _catalog.Add(null));
        }

        [TestMethod]
        public void Indexer_ReturnsItemsInInsertionOrder()
        {
            AddAllThreeDevices();
            Assert.AreSame(_genericDevice, _catalog[0]);
            Assert.AreSame(_monitor, _catalog[1]);
            Assert.AreSame(_keyboard, _catalog[2]);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        public void Indexer_WithIndexOutsideRange_ThrowsArgumentOutOfRangeException(int index)
        {
            AddAllThreeDevices();
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _catalog[index]);
        }

        [TestMethod]
        public void RemoveAt_WithValidIndex_RemovesExactlyThatItem()
        {
            AddAllThreeDevices();
            bool removed = _catalog.RemoveAt(1);
            Assert.IsTrue(removed);
            Assert.AreEqual(2, _catalog.Count);
            Assert.AreSame(_genericDevice, _catalog[0]);
            Assert.AreSame(_keyboard, _catalog[1]);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        public void RemoveAt_WithIndexOutsideRange_ReturnsFalseAndKeepsCatalog(int index)
        {
            AddAllThreeDevices();
            bool removed = _catalog.RemoveAt(index);
            Assert.IsFalse(removed);
            Assert.AreEqual(3, _catalog.Count);
        }

        [TestMethod]
        public void RemoveAt_OnEmptyCatalog_ReturnsFalse()
        {
            Assert.IsFalse(_catalog.RemoveAt(0));
        }

        [TestMethod]
        public void Clear_RemovesAllItems()
        {
            AddAllThreeDevices();
            _catalog.Clear();
            Assert.IsTrue(_catalog.IsEmpty);
            Assert.AreEqual(0, _catalog.Count);
        }

        [TestMethod]
        public void TotalPrice_EqualsSumOfItemPrices()
        {
            AddAllThreeDevices();
            Assert.AreEqual(434.99m, _catalog.TotalPrice);
        }

        [TestMethod]
        public void TotalPrice_DecreasesAfterRemoval()
        {
            AddAllThreeDevices();
            _catalog.RemoveAt(1);
            Assert.AreEqual(135.00m, _catalog.TotalPrice);
        }

        [TestMethod]
        public void GetDescriptions_ReturnsPolymorphicDescriptionForEveryItem()
        {
            AddAllThreeDevices();
            List<string> descriptions = _catalog.GetDescriptions().ToList();
            Assert.AreEqual(3, descriptions.Count);
            StringAssert.StartsWith(descriptions[0], ComputerEquipment.BaseCategoryName);
            StringAssert.StartsWith(descriptions[1], Monitor.MonitorCategoryName);
            StringAssert.StartsWith(descriptions[2], Keyboard.KeyboardCategoryName);
        }

        [TestMethod]
        public void GetItemsOfType_ReturnsOnlyItemsOfRequestedType()
        {
            AddAllThreeDevices();
            _catalog.Add(new Monitor("LG", 199.99m, 24.0, 1920, 1080));
            List<Monitor> monitors = _catalog.GetItemsOfType<Monitor>().ToList();
            List<Keyboard> keyboards = _catalog.GetItemsOfType<Keyboard>().ToList();
            Assert.AreEqual(2, monitors.Count);
            Assert.AreEqual(1, keyboards.Count);
            Assert.AreSame(_keyboard, keyboards[0]);
        }

        [TestMethod]
        public void GetItemsOfType_ForBaseType_ReturnsAllItems()
        {
            AddAllThreeDevices();
            Assert.AreEqual(3, _catalog.GetItemsOfType<ComputerEquipment>().Count());
        }

        [TestMethod]
        public void Enumeration_WithForEach_VisitsAllItems()
        {
            AddAllThreeDevices();
            int visited = 0;
            foreach (ComputerEquipment item in _catalog)
            {
                Assert.IsNotNull(item);
                visited++;
            }

            Assert.AreEqual(3, visited);
        }
    }
}
