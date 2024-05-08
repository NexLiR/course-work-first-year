using Project.Assets.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Assets.ControlClasses
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private Space _space;
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        public GameViewModel()
        {
            var settings = new GameSettings();
            CanvasWidth = settings.GameWidth;
            CanvasHeight = settings.GameHeight;
        }

        public void InitializeGameComponents()
        {
            _space = new Space(1, CanvasWidth, CanvasHeight, false, false, new List<Enemy>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class GameSettings
    {
        public double GameWidth { get; set; }
        public double GameHeight { get; set; }

        public GameSettings()
        {
            GameWidth = 20000;
            GameHeight = 20000;
        }
    }
}
