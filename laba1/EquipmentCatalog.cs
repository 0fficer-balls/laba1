using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace laba1
{
    public class EquipmentCatalog : IEnumerable<ComputerEquipment>
    {
        private readonly List<ComputerEquipment> _items = new List<ComputerEquipment>();
        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }

        public decimal TotalPrice
        {
            get { return _items.Sum(item => item.Price); }
        }

        public ComputerEquipment this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index), index, "Индекс устройства вне диапазона каталога.");
                }

                return _items[index];
            }
        }

        public void Add(ComputerEquipment equipment)
        {
            if (equipment == null)
            {
                throw new ArgumentNullException(nameof(equipment));
            }

            _items.Add(equipment);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                return false;
            }

            _items.RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerable<string> GetDescriptions()
        {
            return _items.Select(item => item.GetDescription());
        }

        public IEnumerable<TEquipment> GetItemsOfType<TEquipment>() where TEquipment : ComputerEquipment
        {
            return _items.OfType<TEquipment>();
        }

        public IEnumerator<ComputerEquipment> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
