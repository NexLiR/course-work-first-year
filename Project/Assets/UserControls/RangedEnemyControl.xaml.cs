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
using System.Windows.Threading;

namespace Project.Assets.UserControls
{
    public partial class RangedEnemyControl : UserControl
    {
        private double maxHP;
        public RangedEnemyControl(double currentHP)
        {
            InitializeComponent();
            maxHP = currentHP;
            HPBar.Maximum = maxHP;
        }
        public void UpdateHP(double currentHP)
        {
            HPBar.Value = currentHP;
        }
    }
}
