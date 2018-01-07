using System;
using System.Windows;
using System.Windows.Input;

namespace BreakOut
{
    public partial class MainWindow : Window
    {
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
                        if (!ballInMove)
                        {
                            ballInMove = true;
                            ballDirectionX = (rnd.Next(10) * Math.Pow(-1, rnd.Next(2))) / 5.0;
                            if (ballDirectionX < 0)
                                ballDirectionY = -2 + ballDirectionX;
                            else
                                ballDirectionY = -2 - ballDirectionX;
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

    }
}