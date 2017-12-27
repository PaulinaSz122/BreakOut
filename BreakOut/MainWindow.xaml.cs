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
using System.Windows.Threading;

namespace BreakOut
{
    public partial class MainWindow : Window
    {
        private bool playClicked = false;
        private bool ballInMove = false;
        private ScoreBox scoreBox;
        private TextBlock scoreBlock;
        private TextBlock livesBlock;
        private Ball ball;
        private Bat bat;
        private List<Brick> brick;
        private int lives = 3;
        private int score = 0;
        private TimeSpan moveBat = new TimeSpan(10000);
        private TimeSpan moveBall = new TimeSpan(10000);
        private int batDirection = 0; //0 - brak ruchu, 4 - lewo, 6 prawo
        private DispatcherTimer timerBat, timerBall;
        private double ballDirectionX, ballDirectionY;
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            timerBat = new DispatcherTimer();
            timerBat.Tick += new EventHandler(TimerTickBat);
            timerBat.Interval = moveBat;
            timerBall = new DispatcherTimer();
            timerBall.Tick += new EventHandler(TimerTickBall);
            timerBall.Interval = moveBall;
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            this.KeyUp += new KeyEventHandler(OnButtonKeyUp);
        }
        private void TimerTickBat(object sender, EventArgs e)
        {
            switch (batDirection)
            {
                case 4:
                    if (bat.batX > 0)
                    {
                        paintCanvas.Children.Remove(bat);
                        bat.batX -= 2;
                        PaintBat();
                        if (!ballInMove)
                        {
                            paintCanvas.Children.Remove(ball);
                            ball.ballX -= 2;
                            PaintBall();
                        }
                    }
                    break;
                case 6:
                    if (bat.batX < 724)
                    {
                        paintCanvas.Children.Remove(bat);
                        bat.batX += 2;
                        PaintBat();
                        if (!ballInMove)
                        {
                            paintCanvas.Children.Remove(ball);
                            ball.ballX += 2;
                            PaintBall();
                        }
                    }
                    break;
            }
        }
        private void TimerTickBall(object sender, EventArgs e)
        {
            if (ballInMove)
            {
                paintCanvas.Children.Remove(ball);
                if (ball.ballX < 0 || ball.ballX > 786)
                {
                    ballDirectionX *= -1;
                }
                else if (ball.ballY < 0 || ball.ballY > 564)
                { 
                    ballDirectionY *= -1;
                }
                foreach(Brick b in brick)
                {
                    if (b.destroyed == false &&
                        ball.ballX >= b.brickX - 25 &&
                        ball.ballX  <= b.brickX &&
                        ball.ballY >= b.brickY - 25 &&
                        ball.ballY <= b.brickY + 25 )
                    {
                        paintCanvas.Children.Remove(b);
                        b.destroyed = true;
                        ballDirectionX *= -1;
                        ballDirectionY *= -1;
                    }
                }
                

                ball.ballX += ballDirectionX;
                ball.ballY += ballDirectionY;
                PaintBall();
            }
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (playClicked)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        batDirection = 4;
                        timerBat.Start();
                        break;
                    case Key.Right:
                        batDirection = 6;
                        timerBat.Start();
                        break;
                    case Key.Space:
                        ballInMove = true;
                        ballDirectionX = (rnd.Next(10) * Math.Pow(-1, rnd.Next(2))) / 5.0;
                        if (ballDirectionX < 0)
                            ballDirectionY = -2 + ballDirectionX;
                        else
                            ballDirectionY = -2 - ballDirectionX;
                        timerBall.Start();
                        break;
                }
            }
        }
        private void OnButtonKeyUp(object sender, KeyEventArgs e)
        {
            if (playClicked)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        batDirection = 0;
                        timerBat.Stop();
                        break;
                    case Key.Right:
                        batDirection = 0;
                        timerBat.Stop();
                        break;
                }
            }
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            playClicked = true;
            paintCanvas.Children.Remove(title);
            paintCanvas.Children.Remove(playImage);
            paintCanvas.Children.Remove(playButton);
            CreateScene();
        }
        private void PaintBall()
        {
            Canvas.SetTop(ball, ball.ballY);
            Canvas.SetLeft(ball, ball.ballX);
            paintCanvas.Children.Add(ball);
        }
        private void PaintBat()
        {
            Canvas.SetTop(bat, bat.batY);
            Canvas.SetLeft(bat, bat.batX);
            paintCanvas.Children.Add(bat);
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
            PaintBall();

            bat = new Bat();
            PaintBat();

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
