using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Microsoft.SqlServer.Server;
using System.Windows.Media;
using Project.Properties;

namespace Project.Assets.Class
{
    public class soundControls
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        MediaPlayer soundPlayer = new MediaPlayer();

        public void musicSound()
        {
            mediaPlayer.Open(new Uri(string.Format("music-1.mp3"), UriKind.Relative));
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
