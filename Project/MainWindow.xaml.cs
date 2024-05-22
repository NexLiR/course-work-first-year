using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using Project.Assets.DataClasses;
using System.IO;
using Project.Assets.ControlClasses;
using Project.Assets.UserControls;
using System.Windows.Threading;

namespace Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            music.PlayMusic();
            UpdateAllMaxScores();
        }

        private void InitializeGame()
        {
            gameSpace = new Space(1, 1920, 1064);
            DataContext = gameSpace;
            UIUpdateTimer.Interval = TimeSpan.FromMilliseconds(6);
            UIUpdateTimer.Tick += UIUpdateTimer_Tick;
            UIUpdateTimer.Start();
            TimeTimer.Interval = TimeSpan.FromSeconds(1);
            TimeTimer.Tick += (sender, e) => currentTime++;
        }

        // Control classes
        public static Player player = new Player();

        Space gameSpace = new Space();
        SavesControls save = new SavesControls();
        SoundControls music = new SoundControls();
        SoundControls sound = new SoundControls();
        private BitmapImage bitmapImage;
        private GameScreen GameScreen;
        private DispatcherTimer UIUpdateTimer = new DispatcherTimer();
        private DispatcherTimer TimeTimer = new DispatcherTimer();
        public static GameControls gameControls;
        public static EnemyControls enemyControls;
        //Data Storage
        public static int currentScore = 0;
        public static int currentTime = 0;
        public static bool isPaused = false;
        public static bool isGameEnded = false;
        public static string currentSave { get; set;}
        public static double currentDifficultyMultiplayer = -1;
        protected int currentCharacter = -1;

        #region ButtonFunctions
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            MainMenu.Visibility = Visibility.Hidden;
            GameSaves.Visibility = Visibility.Visible;
            UpdateAllMaxScores();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            MainMenu.Visibility = Visibility.Hidden;
            SettingsMenu.Visibility = Visibility.Visible;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            Application.Current.Shutdown();
        }
        // Settings menu buttons
        private void Music_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Properties.Settings.Default.musicVolume = (int)Music_Slider.Value;
            Properties.Settings.Default.Save();
        }
        private void Sound_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Properties.Settings.Default.soundVolume = (int)Sound_Slider.Value;
            Properties.Settings.Default.Save();
        }
        private void Return_to_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            SettingsMenu.Visibility = Visibility.Hidden;
            MainMenu.Visibility = Visibility.Visible;
        }
        private void Change_to_English_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            Properties.Settings.Default.languageCode = "en";
        }
        private void Change_to_Ukrainian_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            Properties.Settings.Default.languageCode = "uk";
        }
        private void Dont_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            SaveSettings.Visibility = Visibility.Hidden;
            SettingsMenu.Visibility = Visibility.Visible;
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
        }
        private void Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenu.Visibility = Visibility.Hidden;
            sound.PlaySound("button-click");
            SaveSettings.Visibility = Visibility.Visible;
        }
        //Game save buttons
        private void Load_Save1_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save1.txt") == true)
            {
                currentSave = "save1.txt";
                GameSaves.Visibility = Visibility.Hidden;
                save.ReadSaveData("save1.txt");
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            else
            {
                currentSave = "save1.txt";
                save.CreateSave("save1.txt");
                MessageBox.Show("New save created.");
                GameSaves.Visibility = Visibility.Hidden;
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            UpdateAllMaxScores();
        }
        private void Delete_Save1_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save1.txt") == true)
            {
                save.DeleteSave("save1.txt");
                MessageBox.Show("Save deleted successfully.");
            }
            else
            {
                MessageBox.Show("Save not found.");
            }
            UpdateAllMaxScores();
        }
        private void Load_Save2_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save2.txt") == true)
            {
                currentSave = "save2.txt";
                GameSaves.Visibility = Visibility.Hidden;
                save.ReadSaveData("save2.txt");
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            else
            {
                currentSave = "save2.txt";
                save.CreateSave("save2.txt");
                MessageBox.Show("New save created.");
                GameSaves.Visibility = Visibility.Hidden;
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            UpdateAllMaxScores();
        }
        private void Delete_Save2_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save2.txt") == true)
            {
                save.DeleteSave("save2.txt");
                MessageBox.Show("Save deleted successfully.");
            }
            else
            {
                MessageBox.Show("Save not found.");
            }
            UpdateAllMaxScores();
        }
        private void Load_Save3_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save3.txt") == true)
            {
                currentSave = "save3.txt";
                GameSaves.Visibility = Visibility.Hidden;
                save.ReadSaveData("save3.txt");
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            else
            {
                currentSave = "save3.txt";
                save.CreateSave("save3.txt");
                MessageBox.Show("New save created.");
                GameSaves.Visibility = Visibility.Hidden;
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            UpdateAllMaxScores();
        }
        private void Delete_Save3_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save3.txt") == true)
            {
                save.DeleteSave("save3.txt");
                MessageBox.Show("Save deleted successfully.");
            }
            else
            {
                MessageBox.Show("Save not found.");
            }
            UpdateAllMaxScores();
        }

        //Game start menu buttons
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (currentCharacter != -1 && currentDifficultyMultiplayer != -1)
            {
                CharactersSelectMenu.Visibility = Visibility.Hidden;
                Menu.Visibility = Visibility.Hidden;
                InGameUI.Visibility = Visibility.Visible;
                GameScreen = new GameScreen(bitmapImage);
                GameScreen.GameSpaceLoaded += (o, args) => GameScreen.GameSpace.Focus();
                Game.Children.Add(GameScreen);
                GameScreen.Visibility = Visibility.Visible;
                if (currentCharacter == 1)
                {
                    player = new Player(1, "Character1", 100.0, 1.0, 5.0, 1.2, new Vector(960, 532), 0, 100.0, 1, 2.5);
                }
                else if (currentCharacter == 2)
                {
                    player = new Player(2, "Character2", 200.0, 0.75, 3.5, 1.5, new Vector(960, 532), 0, 200, 2, 10);
                }
                gameSpace = new Space(1, 1920, 1064);
                gameControls = new GameControls(GameScreen, player);
                enemyControls = new EnemyControls(currentDifficultyMultiplayer, GameScreen);

                player.CurrentHealth = player.CurrentHealth * currentDifficultyMultiplayer;
                player.MaxHealth = player.MaxHealth * currentDifficultyMultiplayer;

                Canvas.SetZIndex(InGameUI, 1);

                TimeTimer.Start();
                gameControls.StartGame();
                enemyControls.StartEnemySpawning();
            }
            else
            {
                MessageBox.Show("Please select character and difficulty.");
            }
        }
        private void Choose_Character1_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnCharacter1.Background = Brushes.White;
            btnCharacter1.Foreground = Brushes.Black;
            btnCharacter2.Background = Brushes.Black;
            btnCharacter2.Foreground = Brushes.White;
            currentCharacter = 1;
        }
        private void Choose_Character2_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnCharacter1.Background = Brushes.Black;
            btnCharacter1.Foreground = Brushes.White;
            btnCharacter2.Background = Brushes.White;
            btnCharacter2.Foreground = Brushes.Black;
            currentCharacter = 2;
        }
        private void Easy_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.White;
            btnEasy.Foreground = Brushes.Black;
            btnNormal.Background = Brushes.Black;
            btnNormal.Foreground = Brushes.White;
            btnHard.Background = Brushes.Black;
            btnHard.Foreground = Brushes.White;
            currentDifficultyMultiplayer = 2.0;
            bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Assets/Textures/background-easy.png"));
            Gold.Foreground = Brushes.Black;
            Score.Foreground = Brushes.Black;
            timerText.Foreground = Brushes.Black;
        }
        private void Normal_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.Black;
            btnEasy.Foreground = Brushes.White;
            btnNormal.Background = Brushes.White;
            btnNormal.Foreground = Brushes.Black;
            btnHard.Background = Brushes.Black;
            btnHard.Foreground = Brushes.White;
            currentDifficultyMultiplayer = 1.0;
            bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Assets/Textures/background-normal.png"));
            Gold.Foreground = Brushes.White;
            Score.Foreground = Brushes.White;
            timerText.Foreground = Brushes.White;
        }
        private void Hard_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.Black;
            btnEasy.Foreground = Brushes.White;
            btnNormal.Background = Brushes.Black;
            btnNormal.Foreground = Brushes.White;
            btnHard.Background = Brushes.White;
            btnHard.Foreground = Brushes.Black;
            currentDifficultyMultiplayer = 0.5;
            bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Assets/Textures/background-hard.png"));
            Gold.Foreground = Brushes.White;
            Score.Foreground = Brushes.White;
            timerText.Foreground = Brushes.White;
        }
        #endregion

        #region FuntionalMethods
        private string FormatTime(string rawTime)
        {
            int timeInSeconds = int.Parse(rawTime);
            int minutes = timeInSeconds / 60;
            int seconds = timeInSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }
        private void UpdateMaxScoreAndTime(TextBlock maxScoreTextBlock, string saveFileName, Run textBlockMaxScore, TextBlock maxTimeTextBlock, Run textBlockMaxTime)
        {
            if (save.CheckSaveExistence(saveFileName) == false)
            {
                maxScoreTextBlock.Visibility = Visibility.Hidden;
                maxTimeTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                maxScoreTextBlock.Visibility = Visibility.Visible;
                maxTimeTextBlock.Visibility = Visibility.Visible;
                string saveData = save.ReadSaveData(saveFileName);
                if (saveData != null)
                {
                    string[] saveParts = saveData.Split(';');
                    foreach (string part in saveParts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length == 2)
                        {
                            string key = keyValue[0].Trim();
                            string value = keyValue[1].Trim();
                            if (key == "maxScore")
                            {
                                textBlockMaxScore.Text = value;
                            }
                            else if (key == "maxTime")
                            {
                                textBlockMaxTime.Text = FormatTime(value);
                            }
                        }
                    }
                }
            }
        }
        private void UpdateAllMaxScores()
        {
            UpdateMaxScoreAndTime(maxScore1, "save1.txt", TextBlockMaxScore1, maxTime1, TextBlockMaxTime1);
            UpdateMaxScoreAndTime(maxScore2, "save2.txt", TextBlockMaxScore2, maxTime2, TextBlockMaxTime2);
            UpdateMaxScoreAndTime(maxScore3, "save3.txt", TextBlockMaxScore3, maxTime3, TextBlockMaxTime3);
        }
        private void UIUpdateTimer_Tick(object sender, EventArgs e)
        {
            music.SetMusicVolume();
            if (player.CurrentHealth >= 0)
            {
                PlayerCurrentHealth.Text = Math.Round(player.CurrentHealth).ToString("F0");
            }
            else
            {
                PlayerCurrentHealth.Text = "0";
            }
            PlayerMaxHealth.Text = player.MaxHealth.ToString();
            pbPlayerHealth.Value = player.CurrentHealth;
            pbPlayerHealth.Maximum = player.MaxHealth;
            GoldCount.Text = player.Gold.ToString();
            ScoreCount.Text = currentScore.ToString();
            int minutes = currentTime / 60;
            int seconds = currentTime % 60;
            timerText.Text = $"{minutes:00}:{seconds:00}";
            if (isGameEnded)
            {
                RestartGame();
            }
        }
        public void RestartGame()
        {
            isGameEnded = false;
            isPaused = false;
            TimeTimer.Stop();
            currentTime = 0;
            currentScore = 0;
            currentSave = null;

            GameScreen.GameSpace.Children.Clear();

            GameScreen.Visibility = Visibility.Hidden;
            InGameUI.Visibility = Visibility.Hidden;
            MainMenu.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
        }
        #endregion
    }
}