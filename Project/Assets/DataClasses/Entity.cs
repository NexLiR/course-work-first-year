using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project.Assets.DataClasses
{
    public abstract class Entity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public Vector Position { get; set; }

        public Entity()
        {
            Id = 0;
            Name = "Entity";
            Health = 0;
            Speed = 0;
            Damage = 0;
            AttackSpeed = 0;
            Position = new Vector(0, 0);
        }
        public Entity(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector)
        {
            Id = id;
            Name = name;
            Health = health;
            Speed = speed;
            Damage = damage;
            AttackSpeed = attackSpeed;
            Position = vector;
        }
        public Entity(Entity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Health = entity.Health;
            Speed = entity.Speed;
            Damage = entity.Damage;
            AttackSpeed = entity.AttackSpeed;
            Position = entity.Position;
        }

        public abstract void NotifyPropertyChanged([CallerMemberName] String propertyName = "");

        public abstract void Movement();
    }
}
