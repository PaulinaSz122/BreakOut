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
        public int ballX, ballY;
        public Ball()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("spr_Ball_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.ballX = 407;
            this.ballY = 496;
        }
    }
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
    public class Bat : Image
    {
        public int batX, batY;
        public Bat()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("spr_Bat_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.batX = 372;
            this.batY = 521;
        }
    }
    public class ScoreBox : Image
    {
        public int X, Y;
        public ScoreBox()
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("spr_ScoreBox_0.png", UriKind.Relative);
            b.EndInit();
            Stretch = Stretch.Fill;
            Source = b;
            this.X = 360;
            this.Y = 0;
        }
    }
    public partial class MainWindow : Window
    {
        private ScoreBox scoreBox;
        private Label scoreLabel;
        private Label livesLabel;
        private Ball ball;
        private Bat bat;
        private List<Brick> brick;
        private int lives = 3;
        private int score = 0;

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
        private void createScene()
        {
            scoreBox = new ScoreBox();
            Canvas.SetTop(scoreBox, scoreBox.Y);
            Canvas.SetLeft(scoreBox, scoreBox.X);
            paintCanvas.Children.Add(scoreBox);

            scoreLabel.Content = score.ToString();
            Canvas.SetTop(scoreLabel, scoreBox.Y);
            Canvas.SetLeft(scoreBox, scoreBox.X);

            livesLabel.Content = lives.ToString();
            Canvas.SetTop(scoreBox, scoreBox.Y);
            Canvas.SetLeft(scoreBox, scoreBox.X);

            ball = new Ball();
            Canvas.SetTop(ball, ball.ballY);
            Canvas.SetLeft(ball, ball.ballX);
            paintCanvas.Children.Add(ball);

            bat = new Bat();
            Canvas.SetTop(bat, bat.batY);
            Canvas.SetLeft(bat, bat.batX);
            paintCanvas.Children.Add(bat);

            Brick tmp;
            brick = new List<Brick>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tmp = new Brick(i);
                    tmp.brickX += j * 64;
                    tmp.brickY += i * 32;
                    Canvas.SetTop(tmp, tmp.brickY);
                    Canvas.SetLeft(tmp, tmp.brickX);
                    paintCanvas.Children.Add(tmp);
 
                    brick.Add(tmp);
                }
            }
            
        }
    }
}
