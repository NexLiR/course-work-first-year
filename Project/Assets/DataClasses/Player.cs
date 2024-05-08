using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Project.Assets.DataClasses
{
    public class Player : Entity
    {
        public int Gold { set; get; }
        public List<Item> Items { set; get; }
        public Player()
            : base()
        {
            Gold = 0;
            Items = new List<Item>();
        }
        public Player(int id, string name, double health, double speed, double damage, double attackSpeed, int gold, List<Item> items)
            : base(id, name, health, speed, damage, attackSpeed)
        {
            Gold = gold;
            Items = items;
        }
        public Player(Player player)
            : base(player)
        {
            Gold = player.Gold;
            Items = new List<Item>(player.Items);
        }
    }
}
