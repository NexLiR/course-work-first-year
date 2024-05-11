using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Microsoft.SqlServer.Server;
using System.Windows.Media;
using Project.Properties;
using System.Threading;
using System.Windows.Threading;
using System.Windows;

namespace Project.Assets.ControlClasses
{
    public class SoundControls
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        MediaPlayer soundPlayer = new MediaPlayer();

        private Dispatcher _dispatcher;

        public SoundControls()
        {
            _dispatcher = Application.Current.Dispatcher;
        }

        // Music player
        public async void PlayMusic()
        {
            mediaPlayer.Open(new Uri(@"Assets\Musics\music-1.mp3", UriKind.Relative));
            mediaPlayer.Volume = (double)Properties.Settings.Default.musicVolume / 100.0;
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            await Task.Run(() => KeepPlaying());
        }
        private void KeepPlaying()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
            timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan &&
                mediaPlayer.NaturalDuration.TimeSpan - mediaPlayer.Position < TimeSpan.FromSeconds(1))
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
            }
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }

        // Sound player
        public async void PlaySound(string sound)
        {
            await _dispatcher.InvokeAsync(() =>
            {
                soundPlayer.Open(new Uri(@"Assets\Sounds\" + sound + ".mp3", UriKind.Relative));
                soundPlayer.Volume = (double)Properties.Settings.Default.soundVolume / 100.0;
                soundPlayer.Play();
            });
        }
    }
}