using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.DataClasses
{
    public abstract class InGameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public bool IsHasCollision { get; set; }
        public bool IsInteractable { get; set; }  
        public InGameObject()
        {
            Name = "Object";
            Description = "This is a object.";
            Value = 0;
            IsHasCollision = false;
            IsInteractable = false;
        }
        public InGameObject(string name, string description, int value, bool isHasCollision, bool isInteractable)
        {
            Name = name;
            Description = description;
            Value = value;
            IsHasCollision = isHasCollision;
            IsInteractable = isInteractable;
        }
        public InGameObject(InGameObject inGameObject)
        {
            Name = inGameObject.Name;
            Description = inGameObject.Description;
            Value = inGameObject.Value;
            IsHasCollision = inGameObject.IsHasCollision;
            IsInteractable = inGameObject.IsInteractable;
        }
    }
}
