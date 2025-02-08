using Project.Assets.ControlClasses;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Project.Assets.UserControls
{
    public partial class GameEndControl : UserControl
    {
        private SavesControls save = new SavesControls();
        private SoundControls sound = new SoundControls();
        private string GameEndTimeString { get; set; }
        public int GameEndScore { get; set; }
        public int GameEndTime { get; set; }
        private int SaveMaxScore { get; set; }
        private int SaveMaxTime { get; set; }
		MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

		public GameEndControl()
        {
            GameEndScore = mainWindow.gameState.CurrentScore;
            GameEndTime = mainWindow.gameState.CurrentTime;
            GetSaveData(mainWindow.gameState.CurrentSave);
            InitializeComponent();
            DataContext = this;
        }

        private string FormatTime(int time)
        {
            int minutes = time / 60;
            int seconds = time % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }
        public void Update()
        {
            GameEndTimeString = FormatTime(GameEndTime);
            gameEndTime.Text = GameEndTimeString;
            gameEndScore.Text = GameEndScore.ToString();

            if (NewRecordCheck())
            {
                NewRecord.Visibility = Visibility.Visible;
                UpdateSaveData(mainWindow.gameState.CurrentSave);
            }
            else
            {
                NewRecord.Visibility = Visibility.Hidden;
            }
        }
        private void GetSaveData(string currentSave)
        {
            string saveData = save.ReadSaveData(currentSave);
            if (saveData != null)
            {
                string[] data = saveData.Split(';');
                foreach (var item in data)
                {
                    string[] keyValue = item.Split(':');
                    if (keyValue[0].Trim() == "maxScore")
                    {
                        SaveMaxScore = int.Parse(keyValue[1].Trim());
                    }
                    else if (keyValue[0].Trim() == "maxTime")
                    {
                        SaveMaxTime = int.Parse(keyValue[1].Trim());
                    }
                }
            }
            else
            {
                SaveMaxScore = 0;
                SaveMaxTime = 0;
            }
        }
        private bool NewRecordCheck()
        {
            return GameEndScore > SaveMaxScore || GameEndTime > SaveMaxTime;
        }
        private void UpdateSaveData(string currentSave)
        {
            if (GameEndScore > SaveMaxScore)
            {
                Score.Foreground = Brushes.Red;
                SaveMaxScore = GameEndScore;
            }
            if (GameEndTime > SaveMaxTime)
            {
                Time.Foreground = Brushes.Red;
                SaveMaxTime = GameEndTime;
            }

            string newSaveData = $"maxScore: {SaveMaxScore}; maxTime: {SaveMaxTime};";
            save.WriteSaveData(currentSave, newSaveData);
        }
        private void ToMainManu_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
			mainWindow.gameState.IsGameEnded = true;
        }
    }
}
 