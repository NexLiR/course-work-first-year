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
        public float JumpLenght { set; get; }
        public double MaxHealth { set; get; }

        public List<Bullet> Bullets { get; set; } = new List<Bullet>();

        public Player()
            : base()
        {
            Gold = 0;
        }
        public Player(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector, int gold, float jumpLenght, double maxHealth)
            : base(id, name, health, speed, damage, attackSpeed, vector)
        {
            Gold = gold;
            JumpLenght = jumpLenght;
            MaxHealth = maxHealth;
        } 
        public Player(Player player)
            : base(player)
        {
            Gold = player.Gold;
            JumpLenght = player.JumpLenght;
            MaxHealth = player.MaxHealth;
        }

        public void TakeDamage(double damage)
        {
            sound.PlaySound("player-hurts");
            CurrentHealth -= damage;
        }
    }
}
