using System;
using System.Windows;

namespace BreakOut
{
    public partial class MainWindow : Window
    {
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
                            ball.X -= 2;
                            paintCanvas.Children.Remove(ball);
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
            #region odbijanie piłki od ściany
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
                    lives--;
                    livesBlock.Text = lives.ToString();
                    if (lives == 0)
                    {
                        GameOver();
                    }
                    ResetBallandBat();
                    return;
                }
                #endregion
                #region odbijanie piłki od paletki
                if ((n = IsCollision(bat)) != 0)
                {
                    if (n == 2)
                    {
                        ballDirectionY *= -1;
                        double m;
                        if ((m = (ball.X - bat.X)) > -24 && m <= 36)
                        {
                            ballDirectionX = (m - 36) / 30;
                        }
                        else if (m > 36 && m < 96)
                        {
                            ballDirectionX = (m - 36) / 30;
                        }
                    }
                    else
                    {
                        lives--;
                        livesBlock.Text = lives.ToString();
                        if (lives == 0)
                        {
                            GameOver();
                        }
                        ResetBallandBat();
                        return;
                    }
                }
                #endregion
                #region odbijanie piłki od klocków
                foreach (Brick b in brick)
                {
                    if ((n = IsCollision(b)) != 0)
                    {
                        tmp = b;
                        paintCanvas.Children.Remove(b);
                        if (n == 2 || n == 8)
                        {
                            ballDirectionY *= -1;
                        }
                        if (n == 4 || n == 6)
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
                #endregion

                // przesunięcie piłki
                ball.X += ballDirectionX;
                ball.Y += ballDirectionY;
                PaintBall();
            }
        }

    }
}