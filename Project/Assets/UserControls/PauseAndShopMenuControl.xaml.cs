using Project.Assets.ControlClasses;
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
    public partial class PauseAndShopMenuControl : UserControl
    {
        private SoundControls sound = new SoundControls();
        private DispatcherTimer UITimer = new DispatcherTimer();
		MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
		public PauseAndShopMenuControl()
        {
            InitializeComponent();
            UITimer.Interval = TimeSpan.FromMilliseconds(6);
            UITimer.Tick += UITimer_Tick;
            UITimer.Start();
        }

        public void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            GameControls.IsUnpaused = true;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            GameControls.isPlayerDead = true;
            GameControls.IsUnpaused = true;
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            GoldCount.Text = mainWindow.player.Gold.ToString();
            MaxHealth.Text = Math.Round(mainWindow.player.MaxHealth, 1).ToString("F1");
            Damage.Text = Math.Round(mainWindow.player.Damage, 1).ToString("F1");
            Speed.Text = Math.Round(mainWindow.player.Speed, 1).ToString("F1");
            AttackSpeed.Text = Math.Round(mainWindow.player.AttackSpeed, 1).ToString("F1");
            MaxHealthCost.Text = Math.Round(mainWindow.player.MaxHealth / 2.5 / mainWindow.gameState.CurrentDifficultyMultiplier).ToString("F0");
            DamageCost.Text = Math.Round(mainWindow.player.Damage * 7.5 / mainWindow.gameState.CurrentDifficultyMultiplier).ToString("F0");
            SpeedCost.Text = Math.Round(mainWindow.player.Speed * 25 / mainWindow.gameState.CurrentDifficultyMultiplier).ToString("F0");
            AttackSpeedCost.Text = Math.Round(mainWindow.player.AttackSpeed * 45 / mainWindow.gameState.CurrentDifficultyMultiplier).ToString("F0");

        }
        private void UpgradeMaxHP_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.player.Gold >= mainWindow.player.MaxHealth / 2.5 / mainWindow.gameState.CurrentDifficultyMultiplier)
            {
				mainWindow.player.Gold = mainWindow.player.Gold - (int)(mainWindow.player.MaxHealth / 2.5 / mainWindow.gameState.CurrentDifficultyMultiplier);
				mainWindow.player.MaxHealth += 10 * mainWindow.gameState.CurrentDifficultyMultiplier;
                sound.PlaySound("button-click");
            }
        }
        private void UpgradeDamage_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.player.Gold >= mainWindow.player.Damage * 7.5 / mainWindow.gameState.CurrentDifficultyMultiplier)
            {
				mainWindow.player.Gold = mainWindow.player.Gold - (int)(mainWindow.player.Damage * 7.5 / mainWindow.gameState.CurrentDifficultyMultiplier);
                mainWindow.player.Damage += 1 * mainWindow.gameState.CurrentDifficultyMultiplier;
                sound.PlaySound("button-click");
            }
        }
        private void UpgradeSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.player.Gold >= mainWindow.player.Speed * 25 / mainWindow.gameState.CurrentDifficultyMultiplier)
            {
				mainWindow.player.Gold = mainWindow.player.Gold - (int)(mainWindow.player.Speed * 25 / mainWindow.gameState.CurrentDifficultyMultiplier);
				mainWindow.player.Speed += 0.1 * mainWindow.gameState.CurrentDifficultyMultiplier;
                sound.PlaySound("button-click");
            }
        }
        private void UpgradeAttackSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.player.Gold >= mainWindow.player.AttackSpeed * 45 / mainWindow.gameState.CurrentDifficultyMultiplier)
            {
				mainWindow.player.Gold = mainWindow.player.Gold - (int)(mainWindow.player.AttackSpeed * 45 / mainWindow.gameState.CurrentDifficultyMultiplier);
				mainWindow.player.AttackSpeed += 0.1 * mainWindow.gameState.CurrentDifficultyMultiplier;
                sound.PlaySound("button-click");
            }
        }
    }
}
