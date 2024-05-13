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
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CurrentHealth { get; set; }
        public double Speed { get; set; }
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public Vector Position { get; set; }

        public Entity()
        {
            Id = 0;
            Name = "Entity";
            CurrentHealth = 0;
            Speed = 0;
            Damage = 0;
            AttackSpeed = 0;
            Position = new Vector(0, 0);
        }
        public Entity(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector)
        {
            Id = id;
            Name = name;
            CurrentHealth = health;
            Speed = speed;
            Damage = damage;
            AttackSpeed = attackSpeed;
            Position = vector;
        }
        public Entity(Entity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CurrentHealth = entity.CurrentHealth;
            Speed = entity.Speed;
            Damage = entity.Damage;
            AttackSpeed = entity.AttackSpeed;
            Position = entity.Position;
        }
    }
}
