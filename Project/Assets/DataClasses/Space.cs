﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Project.Assets.DataClasses
{
    public class Space
    {
        public int SpaceID { set; get; }
        public double Width { set; get; }
        public double Height { set; get; }
        public Space()
        {
            SpaceID = 0;
            Width = 0;
            Height = 0;
        }
        public Space(int spaceId, double width, double height)
        {
            SpaceID = spaceId;
            Width = width;
            Height = height;
        }
        public Space(Space space)
        {
            SpaceID = space.SpaceID;
            Width = space.Width;
            Height = space.Height;
        }
    }
}
