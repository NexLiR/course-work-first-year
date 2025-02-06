using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Project.Assets.DataClasses
{
    public class DifficultySetting
    {
        public double Multiplier { get; set; }

        public string BackgroundUri { get; set; }

        public Brush TextForeground { get; set; }
    }
}
