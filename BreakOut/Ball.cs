using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BreakOut
{
    public class Ball : Sprite
    {
        public Ball()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("Images/spr_Ball_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.X = 407;
            this.Y = 494;
            this.Width = 24;
            this.Height = 24;
        }
    }
}
