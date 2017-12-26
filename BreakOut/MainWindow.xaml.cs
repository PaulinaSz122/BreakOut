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
    public partial class MainWindow : Window
    {
       // private int n = 3;
        private ScoreBox scoreBox;
        private TextBlock scoreBlock;
        private TextBlock livesBlock;
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
            paintCanvas.Children.Remove(title);
            paintCanvas.Children.Remove(playImage);
            paintCanvas.Children.Remove(playButton);
            CreateScene();
        }
        private void CreateScene()
        {
            scoreBox = new ScoreBox();
            Canvas.SetTop(scoreBox, scoreBox.Y);
            Canvas.SetLeft(scoreBox, scoreBox.X);
            paintCanvas.Children.Add(scoreBox);

            scoreBlock = new TextBlock
            {
                Text = score.ToString(),
                Foreground = Brushes.White,
                Height = 16,
                Width = 42,
                FontSize = 14
            };
            Canvas.SetTop(scoreBlock, 3);
            Canvas.SetLeft(scoreBlock, 422);
            paintCanvas.Children.Add(scoreBlock);

            livesBlock = new TextBlock
            {
                Text = lives.ToString(),
                Foreground = Brushes.White,
                Height = 16,
                Width = 42,
                FontSize = 14
            };
            Canvas.SetTop(livesBlock, 19);
            Canvas.SetLeft(livesBlock, 422);
            paintCanvas.Children.Add(livesBlock);

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
