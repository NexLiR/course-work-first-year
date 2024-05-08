using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Project.Assets.DataClasses
{
    public abstract class Enemy : Entity
    {
        public int ScoreValue { get; set; }
        public Enemy()
            : base()
        {
            ScoreValue = 0;
        }
        public Enemy(int id, string name, double health, double speed, double damage, double attackSpeed, int scoreValue)
            : base(id, name, health, speed, damage, attackSpeed)
        {
            ScoreValue = scoreValue;
        }
        public Enemy(Enemy enemy)
            : base(enemy)
        {
            ScoreValue = enemy.ScoreValue;
        }
    }
}
