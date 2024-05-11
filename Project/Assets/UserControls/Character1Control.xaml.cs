using Project.Assets.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Assets.ControlClasses;
using System.Xml.Linq;
using System.Windows.Threading;

namespace Project.Assets.UserControls
{
    public partial class Character1Control : UserControl
    {
        private Player _player;


        public Character1Control(Player player)
        {
            InitializeComponent();
            _player = player;
            DataContext = _player;
        }
    }
}