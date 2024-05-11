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

        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        private float SpeedX, SpeedY, Friction = 0.75f, Speed = (float)character1.Speed;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            Loaded += Window_Loaded;

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SoundControls music = new SoundControls();
            music.PlayMusic();
            UpdateMaxScore();
        }

        private void InitializeGame()
        {
            gameSpace = new Space(1, 1920, 1064, false, false, new List<Enemy>());
            GameScreen.Children.Add(character1Control);
            character1Control.RenderTransform = new TranslateTransform(character1.Position.X, character1.Position.Y);
            DataContext = gameSpace;
        }

        // Control classes
        static Player character1 = new Player(1, "Character1", 100.0, 3.0, 1.0, 1.0, new Vector(800, 400), 0, new List<Item>());
        Space gameSpace = new Space();
        SavesControls save = new SavesControls();
        SoundControls sound = new SoundControls();
        UserControl character1Control = new Character1Control(character1);

        #region ButtonFunctions
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            MainMenu.Visibility = Visibility.Hidden;
            GameSaves.Visibility = Visibility.Visible;
            UpdateMaxScore();
        }
        private void ContinueGame_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            if (save.CheckSaveExistence("save1.txt") == true)
            {
                MainMenu.Visibility = Visibility.Hidden;
                save.ReadSaveData("save1.txt");
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            else
            {
                MainMenu.Visibility = Visibility.Hidden;
                GameSaves.Visibility = Visibility.Visible;
            }
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
        }
        private void Sound_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Properties.Settings.Default.soundVolume = (int)Sound_Slider.Value;
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
                GameSaves.Visibility = Visibility.Hidden;
                save.ReadSaveData("save1.txt");
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
            else
            {
                save.CreateSave("save1.txt");
                MessageBox.Show("New save created.");
                GameSaves.Visibility = Visibility.Hidden;
                CharactersSelectMenu.Visibility = Visibility.Visible;
            }
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

        }
        //private void Load_Save2_Click(object sender, RoutedEventArgs e)
        //{
        //    sound.PlaySound("button-click");
        //}
        //private void Load_Save3_Click(object sender, RoutedEventArgs e)
        //{
        //    sound.PlaySound("button-click");
        //}
        //Game start menu buttons
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            CharactersSelectMenu.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            GameScreen.Visibility = Visibility.Visible;
            GameScreen.Focus();
        }
        private void Choose_Character1_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnCharacter1.Background = Brushes.Green;
            //btnCharacter2.Background = Brushes.Transparent;
            //btnCharacter3.Background = Brushes.Transparent;
        }
        //private void Choose_Character2_Click(object sender, RoutedEventArgs e)
        //{
        //    sound.PlaySound("button-click");
        //    btnCharacter1.Background = Brushes.Transparent;
        //    btnCharacter2.Background = Brushes.Green;
        //    btnCharacter3.Background = Brushes.Transparent;
        //}
        //private void Choose_Character3_Click(object sender, RoutedEventArgs e)
        //{
        //    sound.PlaySound("button-click");
        //    btnCharacter1.Background = Brushes.Transparent;
        //    btnCharacter2.Background = Brushes.Transparent;
        //    btnCharacter3.Background = Brushes.Green;
        //}
        private void Easy_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.Green;
            btnNormal.Background = Brushes.Transparent;
            btnHard.Background = Brushes.Transparent;
        }
        private void Normal_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.Transparent;
            btnNormal.Background = Brushes.Green;
            btnHard.Background = Brushes.Transparent;
        }
        private void Hard_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = Brushes.Transparent;
            btnNormal.Background = Brushes.Transparent;
            btnHard.Background = Brushes.Green;
        }
        #endregion

        //Functional methods
        private void UpdateMaxScore()
        {
            if (save.CheckSaveExistence("save1.txt") == false)
            {
                maxScore.Visibility = Visibility.Hidden;
            }
            else
            {
                maxScore.Visibility = Visibility.Visible;
                string saveData = save.ReadSaveData("save1.txt");
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
                                TextBlockMaxScore.Text = value;
                                break;
                            }
                        }
                    }
                }
            }
        }

        #region GameKeyEvents
        

        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = true;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = true;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = true;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = true;
            }
        }

        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = false;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = false;
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (UpKeyPressed)
            {
                SpeedY += Speed;
            }
            if (DownKeyPressed)
            {
                SpeedY -= Speed;
            }
            if (LeftKeyPressed)
            {
                SpeedX -= Speed;
            }
            if (RightKeyPressed)
            {
                SpeedX += Speed;
            }

            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;

            var transform = character1Control.RenderTransform as TranslateTransform;
            transform.X += SpeedX;
            transform.Y -= SpeedY;
        }
        #endregion
    }
}