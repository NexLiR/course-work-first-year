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
using System.Xml;
using Project.Assets.Class;

namespace Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            soundControls music = new soundControls();
            music.musicSound();
        }

        // Main menu buttons
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            GameSaves.Visibility = Visibility.Visible;
        }
        private void ContinueGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            CharactersSelectMenu.Visibility = Visibility.Visible;
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Hidden;
            SettingsMenu.Visibility = Visibility.Visible;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // Settings menu buttons
        private void Return_to_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenu.Visibility = Visibility.Hidden;
            MainMenu.Visibility = Visibility.Visible;
        }
        private void Change_to_English_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.languageCode = "en";
            Properties.Settings.Default.Save();
            
            ChangingLanguage.Visibility = Visibility.Visible;
        }
        private void Change_to_Ukrainian_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.languageCode = "uk";
            Properties.Settings.Default.Save();

            ChangingLanguage.Visibility = Visibility.Visible;
        }
        private void Dont_Change_language_Click(object sender, RoutedEventArgs e)
        {
            ChangingLanguage.Visibility = Visibility.Hidden;
        }
        private void Change_language_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //Game save buttons
        private void Load_Save1_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Load_Save2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Load_Save3_Click(object sender, RoutedEventArgs e)
        {

        }
        //Game start menu buttons
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
     
        }
        private void Choose_Character1_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Choose_Character2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Choose_Character3_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Easy_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Normal_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Hard_Difficulty_Selected_Click(object sender, RoutedEventArgs e)
        {

        }
    }
  
}
