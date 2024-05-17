using Project.Assets.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Assets.DataClasses
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(int id, string name, double health, double speed, double damage, double attackSpeed, Vector position, int scoreValue, int goldValue, UserControl userControl)
            : base(id, name, health, speed, damage, attackSpeed, position, scoreValue, goldValue, userControl)
        {

        }
        public BasicEnemy(BasicEnemy enemy)
            : base(enemy)
        {

        }
        public override void Movement()
        {
            
        }

        public override void Attack()
        {
            
        }
    }
}
