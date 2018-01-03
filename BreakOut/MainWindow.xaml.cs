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
        private bool spacePressed = false;

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
                    if (bat.X > 0)
                    {
                        paintCanvas.Children.Remove(bat);
                        bat.X -= 2;
                        PaintBat();
                        if (!ballInMove)
                        {
                            paintCanvas.Children.Remove(ball);
                            ball.X -= 2;
                            PaintBall();
                        }
                    }
                    break;
                case 6:
                    if (bat.X < 724)
                    {
                        paintCanvas.Children.Remove(bat);
                        bat.X += 2;
                        PaintBat();
                        if (!ballInMove)
                        {
                            paintCanvas.Children.Remove(ball);
                            ball.X += 2;
                            PaintBall();
                        }
                    }
                    break;
            }
        }
        private void TimerTickBall(object sender, EventArgs e)
        {
            Brick tmp = null;
            int n;
            if (ballInMove)
            {
                paintCanvas.Children.Remove(ball);
                if (ball.X < 0 || ball.X > 786)
                {
                    ballDirectionX *= -1;
                }
                else if (ball.Y < 0)
                { 
                    ballDirectionY *= -1;
                }
                else if (ball.Y > 564)
                {
                    spacePressed = false;
                    lives--;
                    livesBlock.Text = lives.ToString();
                    if (lives == 0)
                    {
                        GameOver();
                    }
                    Reset();
                    return;
                }
                if ((n = IsCollision(bat)) != 0)
                {
                    if (n == 2)
                    {
                        ballDirectionY *= -1;
                    }
                    else
                    {
                        lives--;
                        livesBlock.Text = lives.ToString();
                        if (lives == 0)
                        {
                            GameOver();
                        }
                        Reset();
                    }
                    
                }
                foreach(Brick b in brick)
                {
                    if ((n = IsCollision(b)) != 0)
                    {
                        tmp = b;
                        paintCanvas.Children.Remove(b);
                        if (n == 2 || n == 8)
                        {
                            ballDirectionY *= -1;
                        }
                        if (n == 4 || n==6)
                        {
                            ballDirectionX *= -1;
                        }
                        score += 10;
                        scoreBlock.Text = score.ToString();
                        break;
                    }
                    
                }
                if (tmp != null)
                brick.Remove(tmp);
                if (brick.Count == 0)
                {
                    MessageBox.Show("You won! Your score is: " + score.ToString(), "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Hand);
                    this.Close();
                    timerBall.Stop();
                    timerBat.Stop();
                }

                ball.X += ballDirectionX;
                ball.Y += ballDirectionY;
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
                        if (!spacePressed)
                        {
                            ballInMove = true;
                            spacePressed = true;
                            ballDirectionX = (rnd.Next(10) * Math.Pow(-1, rnd.Next(2))) / 5.0;
                            if (ballDirectionX < 0)
                            {
                                ballDirectionY = -2 + ballDirectionX;
                            }
                            else
                            {
                                ballDirectionY = -2 - ballDirectionX;
                            }
                            timerBall.Start();
                        }
                        
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
            Canvas.SetTop(ball, ball.Y);
            Canvas.SetLeft(ball, ball.X);
            paintCanvas.Children.Add(ball);
        }
        private void PaintBat()
        {
            Canvas.SetTop(bat, bat.Y);
            Canvas.SetLeft(bat, bat.X);
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
                    tmp.X += j * 64;
                    tmp.Y += i * 32;
                    Canvas.SetTop(tmp, tmp.Y);
                    Canvas.SetLeft(tmp, tmp.X);
                    paintCanvas.Children.Add(tmp);
                    brick.Add(tmp);
                }
            } 
        }
        private int IsCollision(Sprite B)
        {
            //The sides of the rectangles
            double leftBall, leftB;
            double rightBall, rightB;
            double topBall, topB;
            double bottomBall, bottomB;
            
            leftBall = ball.X;
            rightBall = ball.X + ball.Width;
            topBall = ball.Y;
            bottomBall = ball.Y + ball.Height;
            
            leftB = B.X;
            rightB = B.X + B.Width;
            topB = B.Y;
            bottomB = B.Y + B.Height;
            int colisionTop = 2;
            int colisionBottom = 8;
            int colisonLeft = 4;
            int colisionRight = 6;
            
            if (bottomBall <= topB)
            {
                colisionTop = 0;
                return 0;
            }

            if (topBall > bottomB)
            {
                colisionBottom = 0;
                return 0;
            }

            if (rightBall < leftB)
            {
                colisonLeft = 0;
                return 0;
            }

            if (leftBall > rightB)
            {
                colisionRight = 0;
                return 0;
            }
            
            if (colisionTop == 2) return colisionTop;
            if (colisionBottom == 8) return colisionBottom;
            if (colisonLeft == 4) return colisonLeft;
            else return colisionRight;
        }
        private void GameOver()
        {
            MessageBox.Show("You lose! Your score is: " +  score.ToString(), "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            this.Close();
            timerBall.Stop();
            timerBat.Stop();
        }
        private void Reset()
        {
            paintCanvas.Children.Remove(ball);
            paintCanvas.Children.Remove(bat);
            ball.X = 409;
            ball.Y = 494;
            ballInMove = false;
            bat.X = 372;
            bat.Y = 521;
            PaintBat();
            PaintBall();
        }
    }
}
