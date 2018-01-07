using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BreakOut
{
    public class ScoreBox : Sprite
    {
        public ScoreBox()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Images/spr_ScoreBox_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.X = 360;
            this.Y = 0;
        }
    }
}
