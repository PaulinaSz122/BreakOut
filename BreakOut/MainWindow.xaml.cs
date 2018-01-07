using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BreakOut
{
    public partial class MainWindow : Window
    {
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
        private void GameOver()
        {
            MessageBox.Show("You lose! Your score is: " +  score.ToString(), "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            this.Close();
            timerBall.Stop();
            timerBat.Stop();
        }
    }
}
