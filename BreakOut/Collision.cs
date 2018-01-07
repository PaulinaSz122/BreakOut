using System;
using System.Windows;

namespace BreakOut
{
    public partial class MainWindow : Window
    {

        private int IsCollision(Sprite B)
        {
            double leftBall, leftB;
            double rightBall, rightB;
            double topBall, topB;
            double bottomBall, bottomB;

            //obliczenie współrzędnych piłki
            leftBall = ball.X;
            rightBall = ball.X + ball.Width;
            topBall = ball.Y;
            bottomBall = ball.Y + ball.Height;

            //obliczenie współrzędnych bloku/paletki
            leftB = B.X;
            rightB = B.X + B.Width;
            topB = B.Y;
            bottomB = B.Y + B.Height;

            if (bottomBall < topB || topBall > bottomB || rightBall < leftB || leftBall > rightB)
            {
                //sprawdzamy czy nie ma kolizji, jeśli jest zwracamy 0
                return 0;
            }

            // jeśli jest kolizja, sprawdzamy z której strony
            // nadal problemy z uderzeniem od dołu
            if ((Math.Abs(rightBall - leftB)) > Math.Abs(bottomBall - topB))
            {
                if ((Math.Abs(bottomBall - topB)) > Math.Abs(leftBall - rightB))
                {
                    return 6; //prawo
                }
                return 2;//góra
            }
            else if ((Math.Abs(rightBall - leftB)) > Math.Abs(topBall - bottomB))
            {
                return 8;//dół
            }
            else if ((Math.Abs(bottomBall - topB)) > Math.Abs(rightBall - leftB))
            {
                return 4;//lewo
            }

            return 2;
        }
    }
}