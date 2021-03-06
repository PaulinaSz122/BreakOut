﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BreakOut
{
    public class Brick : Sprite
    {
        static string[] brickColor = {"spr_Brick_0.png", "spr_Brick_1.png", "spr_Brick_2.png", "spr_Brick_3.png",
                                      "spr_Brick_4.png", "spr_Brick_5.png", "spr_Brick_6.png"};
        public bool destroyed = false;
        public Brick(int index)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Images/" + brickColor[index], UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.X = 130;
            this.Y = 78;
            this.Width = 64;
            this.Height = 32;
        }
    }
}
