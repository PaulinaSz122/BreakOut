using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
    }
}