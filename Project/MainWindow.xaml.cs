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
using Project.Assets.Class;
using Project.Assets.DataClasses;
using System.IO;
using Project.Assets.ControlClasses;
using Project.Assets.UserControls;

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
            SoundControls music = new SoundControls();
            music.PlayMusic();
            UpdateMaxScore();
            _viewModel.InitializeGameComponents();
        }

        private void InitializeGame()
        {

        }

        // Control classes
        GameViewModel _viewModel = new GameViewModel();
        SavesControls save = new SavesControls();
        SoundControls sound = new SoundControls();

        // Main menu buttons
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
            Game.Visibility = Visibility.Visible;
        }
        private void Choose_Character1_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnCharacter1.Background = System.Windows.Media.Brushes.Green;
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
            btnEasy.Background = System.Windows.Media.Brushes.Green;
            btnNormal.Background = System.Windows.Media.Brushes.Transparent;
            btnHard.Background = System.Windows.Media.Brushes.Transparent;
        }
        private void Normal_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = System.Windows.Media.Brushes.Transparent;
            btnNormal.Background = System.Windows.Media.Brushes.Green;
            btnHard.Background = System.Windows.Media.Brushes.Transparent;
        }
        private void Hard_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {
            sound.PlaySound("button-click");
            btnEasy.Background = System.Windows.Media.Brushes.Transparent;
            btnNormal.Background = System.Windows.Media.Brushes.Transparent;
            btnHard.Background = System.Windows.Media.Brushes.Green;
        }

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
    }
}