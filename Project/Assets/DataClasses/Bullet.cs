using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Assets.DataClasses
{
    public class Bullet
    {
        public Point Position { get; set; }
        public Vector Direction { get; set; }
        public double Speed { get; set; }
        public double LifeTime { get; set; }

        public Bullet(Point position, Vector direction, double speed, double lifeTime)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            LifeTime = lifeTime;
        }
    }
}
