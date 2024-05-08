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

namespace Project.Assets.UserControls
{
    public partial class Character1Control : UserControl
    {
        public Character1Control()
        {
            InitializeComponent();
            Player character_1 = new Player(1, "character-1", 100, 1, 1, 1, 0, new List<Item>());
        }
    }
}
