using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BreakOut
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public class Ball : Image
    {

        public Ball()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("spr_Ball_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
        }
    }
    public class Brick : Image
    {
        static string[] brickColor = {"spr_Brick_0.png", "spr_Brick_1.png", "spr_Brick_2.png", "spr_Brick_3.png",
                                      "spr_Brick_4.png", "spr_Brick_5.png", "spr_Brick_6.png"};
        public Brick(int index)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(brickColor[index], UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
        }
    }
    public class Bat : Image
    {
        public Bat()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("spr_Ball_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
        }
    }
    public partial class MainWindow : Window
    {
        public void createScene()
        {
            Ball ball = new Ball();
            Canvas.SetTop(ball, 40);
            Canvas.SetRight(ball, 40);
            paintCanvas.Children.Add(ball);

            for (int i = 0; i < 7; i++)
            {

            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            title.Opacity = 0;
            playImage.Opacity = 0;
            createScene();
        }
    }
}
