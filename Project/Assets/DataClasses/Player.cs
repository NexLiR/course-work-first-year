using Project.Assets.ControlClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Xml.Linq;

namespace Project.Assets.DataClasses
{
    public class Player : Entity
    {
        protected SoundControls sound = new SoundControls();
        public int Gold { set; get; }
        public double MaxHealth { set; get; }
        public int UltimateID { set; get; }
        public double UltimateCooldown { set; get; }

        public List<Bullet> Bullets { get; set; } = new List<Bullet>();

        public Player()
            : base()
        {
            Gold = 0;
            MaxHealth = 0;
            UltimateID = 0;
            UltimateCooldown = 0;
        }
        public Player(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector, int gold, double maxHealth, int ultimateID, double ultimateCooldown)
            : base(id, name, health, speed, damage, attackSpeed, vector)
        {
            Gold = gold;
            MaxHealth = maxHealth;
            UltimateID = ultimateID;
            UltimateCooldown = ultimateCooldown;
        } 
        public Player(Player player)
            : base(player)
        {
            Gold = player.Gold;
            MaxHealth = player.MaxHealth;
            UltimateID = player.UltimateID;
            UltimateCooldown = player.UltimateCooldown;
        }

        public void TakeDamage(double damage)
        {
            sound.PlaySound("player-hurts");
            CurrentHealth -= damage;
        }
    }
}
