using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public Player(int id, string name, double health, double speed, double damage, double attackSpeed, Vector vector, int gold, List<Item> items)
            : base(id, name, health, speed, damage, attackSpeed, vector)
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
        public override async void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            await Task.Run(() =>
            {
                
            });
        }
        public override void Movement()
        {

        }
    }
}
