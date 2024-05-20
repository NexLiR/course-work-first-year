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

namespace Project.Assets.UserControls
{
    public partial class GameScreen : UserControl
    {
        public event EventHandler GameSpaceLoaded;
        ImageBrush backgroundImage = new ImageBrush();
        public GameScreen(BitmapImage bitMapImage)
        {
            InitializeComponent();
            Loaded += GameScreen_Loaded;
            backgroundImage.ImageSource = bitMapImage;
            GameSpace.Background = backgroundImage;
        }
        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            GameSpaceLoaded?.Invoke(sender, e);
        }
    }
}
