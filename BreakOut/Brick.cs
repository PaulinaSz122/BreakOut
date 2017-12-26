using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace BreakOut
{
    public class Brick : Image
    {
        static string[] brickColor = {"spr_Brick_0.png", "spr_Brick_1.png", "spr_Brick_2.png", "spr_Brick_3.png",
                                      "spr_Brick_4.png", "spr_Brick_5.png", "spr_Brick_6.png"};
        public int brickX, brickY;
        public Brick(int index)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(brickColor[index], UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.brickX = 130;
            this.brickY = 82;
        }
    }
}
