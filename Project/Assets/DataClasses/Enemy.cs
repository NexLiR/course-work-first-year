using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace Project.Assets.DataClasses
{
    public abstract class Enemy : Entity
    {
        public int ScoreValue { get; set; }
        public int GoldValue { get; set; }
        public UserControl UserControl { get; set; }
        public Enemy()
            : base()
        {
            ScoreValue = 0;
            GoldValue = 0;
            UserControl = null;
        }
        public Enemy(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector, int scoreValue, int goldValue, UserControl userControl)
            : base(id, name, health, speed, damage, attackSpeed, vector)
        {
            ScoreValue = scoreValue;
            GoldValue = goldValue;
            UserControl = userControl;
        }
        public Enemy(Enemy enemy)
            : base(enemy)
        {
            ScoreValue = enemy.ScoreValue;
            GoldValue = enemy.GoldValue;
            UserControl = enemy.UserControl;
        }
        public abstract void Movement();
        public abstract void Attack();
    }
}
