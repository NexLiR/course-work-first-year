using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.DataClasses
{
    public abstract class WeaponItem : Item
    {
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public double AttackRange { get; set; }
        public double ReloadTime { get; set; }
        public double AmmoQuantity { get; set; }

        public WeaponItem()
            :base()
        {
            Damage = 0;
            AttackSpeed = 0;
            AttackRange = 0;
            ReloadTime = 0;
            AmmoQuantity = 0;
        }
        public WeaponItem(string name, string description, int value, double damage, double attackSpeed, double attackRange, double reloadTime, double ammoQuantity)
            :base(name, description, value)
        {
            Damage = damage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
            ReloadTime = reloadTime;
            AmmoQuantity = ammoQuantity;
        }
        public WeaponItem(WeaponItem weaponItem)
            :base(weaponItem)
        {
            Damage = weaponItem.Damage;
            AttackSpeed = weaponItem.AttackSpeed;
            AttackRange = weaponItem.AttackRange;
            ReloadTime = weaponItem.ReloadTime;
            AmmoQuantity = weaponItem.AmmoQuantity;
        }
    }
}