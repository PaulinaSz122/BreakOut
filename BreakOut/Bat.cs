﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BreakOut
{
    public class Bat : Sprite
    {
        public Bat()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Images/spr_Bat_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.X = 372;
            this.Y = 521;
            this.Width = 96;
            this.Height = 32;
        }
    }
}
