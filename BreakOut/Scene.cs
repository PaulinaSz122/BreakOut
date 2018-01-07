using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BreakOut
{
    public partial class MainWindow : Window
    {
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
            #region utworzenie tabelki z wynikiem
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
            #endregion

            // utworzenie piłki
            ball = new Ball();
            PaintBall();

            //utworzenie paletki
            bat = new Bat();
            PaintBat();

            // utworzenie klocków
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
        private void ResetBallandBat()
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