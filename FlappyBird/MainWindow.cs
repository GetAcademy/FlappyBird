using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlappyBird
{
    public class MainWindow : Window
    {
        private const int BallSize = 20;
        private const int BallSpeed = 5;

        private Ellipse ball;
        private Canvas gameCanvas;
        private DispatcherTimer gameTimer;

        private double ballXSpeed = BallSpeed;
        private double ballYSpeed = -BallSpeed;

        public MainWindow()
        {
            // Initial setup
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Create a canvas
            gameCanvas = new Canvas();
            gameCanvas.Background = Brushes.Black;
            Content = gameCanvas;

            // Create a ball
            ball = CreateBall();
            gameCanvas.Children.Add(ball);

            // Create a timer for the game loop
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;

            // Start the game
            StartGame();
        }

        private Ellipse CreateBall()
        {
            var ellipse = new Ellipse
            {
                Width = BallSize,
                Height = BallSize,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            // Set initial position
            Canvas.SetLeft(ellipse, gameCanvas.ActualWidth / 2 - BallSize / 2);
            Canvas.SetTop(ellipse, gameCanvas.ActualHeight / 2 - BallSize / 2);

            return ellipse;
        }

        private void StartGame()
        {
            // Reset ball position
            Canvas.SetLeft(ball, gameCanvas.ActualWidth / 2 - BallSize / 2);
            Canvas.SetTop(ball, gameCanvas.ActualHeight / 2 - BallSize / 2);

            // Start the game timer
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Move the ball
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballXSpeed);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballYSpeed);

            // Check for collisions with walls
            CheckWallCollisions();
        }

        private void CheckWallCollisions()
        {
            // Check left wall
            if (Canvas.GetLeft(ball) <= 0)
            {
                ballXSpeed = Math.Abs(ballXSpeed); // Reflect the ball to the right
            }

            // Check top wall
            if (Canvas.GetTop(ball) <= 0)
            {
                ballYSpeed = Math.Abs(ballYSpeed); // Reflect the ball downward
            }

            // Check right wall
            if (Canvas.GetLeft(ball) >= gameCanvas.ActualWidth - BallSize)
            {
                ballXSpeed = -Math.Abs(ballXSpeed); // Reflect the ball to the left
            }

            // Check bottom wall
            if (Canvas.GetTop(ball) >= gameCanvas.ActualHeight - BallSize)
            {
                ballYSpeed = -Math.Abs(ballYSpeed); // Reflect the ball upward
            }
        }

    }
}