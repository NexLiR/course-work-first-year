using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.DataClasses
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public bool IsActive { get; set; }

        public Item()
        {
            Name = "Item";
            Description = "This is a item.";
            Value = 0;
            IsActive = false;
        }
        public Item(string name, string description, int value, bool isActive)
        {
            Name = name;
            Description = description;
            Value = value;
            IsActive = isActive;
        }
        public Item(string name, string description, int value)
        {
            Name = name;
            Description = description;
            Value = value;
        }
        public Item(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            Value = item.Value;
            IsActive = item.IsActive;
        }
    }
}