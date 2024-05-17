using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Assets.DataClasses
{
    public class Bullet
    {
        public Point Position { get; set; }
        public Vector Direction { get; set; }
        public double Speed { get; set; }
        public double LifeTime { get; set; }
        public double Damage { get; set; }
        public UserControl UserControl { get; set; }

        public Bullet(Point position, Vector direction, double speed, double lifeTime, double damage, UserControl userControl)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            LifeTime = lifeTime;
            Damage = damage;
            UserControl = userControl;
        }
    }
}
