using Project.Assets.DataClasses;
using Project.Assets.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Project.Assets.ControlClasses
{
    public class GameControls
    {
        private SoundControls sound = new SoundControls();
        public static bool IsPaused = false;
        public static bool IsUnpaused = false;
        public static bool isPlayerDead = false;
        public GameScreen GameScreen { get; set; }
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed, LeftMouseButtonPressed;
        private float SpeedX, SpeedY, Friction = 0.75f, Speed;
        private Point mousePosition;

        public UserControl playerControl { get; set;}
        private static Player player;

        private TranslateTransform translateTransform;
        private RotateTransform rotateTransform;
        private TransformGroup combinedTransform;

        private Vector movementDirection;
        private bool ultimateAvailable = true, attackAvalible = true;
        private DispatcherTimer UltimateTimer = new DispatcherTimer();

        private DispatcherTimer AttackTimer = new DispatcherTimer();
        private Vector facingDirection;

        private DispatcherTimer RegenerationTimer = new DispatcherTimer();

        private GameEndControl endGameScreen;

        private PauseAndShopMenuControl pauseAndShopMenu;
        private DispatcherTimer UnpauseCheckTimer = new DispatcherTimer();

        private DispatcherTimer Ultimate2DurationTimer = new DispatcherTimer();
		MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

		public GameControls(GameScreen gameScreen, Player character)
        {
            player = character;
            Speed = (float)player.Speed;
            GameScreen = gameScreen;
            if (player.Id == 1)
            {
                playerControl = new Character1Control(player);
            }
            if (player.Id == 2)
            {
                playerControl = new Character2Control(player);
            }
            GameScreen.GameSpace.Children.Add(playerControl);

            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            combinedTransform = new TransformGroup();
            combinedTransform.Children.Add(rotateTransform);
            combinedTransform.Children.Add(translateTransform);
            playerControl.RenderTransform = combinedTransform;

            movementDirection = new Vector(1, 0);

            facingDirection = new Vector(1, 0);

            endGameScreen = new GameEndControl();
            pauseAndShopMenu = new PauseAndShopMenuControl();
        }
        public void StartGame()
        {
            translateTransform.X = player.Position.X;
            translateTransform.Y = player.Position.Y;
            GameScreen.GameSpace.Focus();

            GameScreen.GameSpace.KeyDown += KeyboardDown;
            GameScreen.GameSpace.KeyUp += KeyboardUp;
            GameScreen.GameSpace.MouseMove += GameScreen_MouseMove;

            GameScreen.GameSpace.MouseDown += MouseDown;
            GameScreen.GameSpace.MouseUp += MouseUp;

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();

            UltimateTimer.Interval = TimeSpan.FromSeconds(player.UltimateCooldown);
            UltimateTimer.Tick += UltimateTimer_Tick;
            UltimateTimer.Start();

            AttackTimer.Interval = TimeSpan.FromSeconds(1.0 / player.AttackSpeed);
            AttackTimer.Tick += AttackTimer_Tick;
            AttackTimer.Start();

            RegenerationTimer.Interval = TimeSpan.FromSeconds(1);
            RegenerationTimer.Tick += RegenerationTimer_Tick;
            RegenerationTimer.Start();

            UnpauseCheckTimer.Interval = TimeSpan.FromMilliseconds(6);
            UnpauseCheckTimer.Tick += UnpauseCheckTimer_Tick;
            UnpauseCheckTimer.Start();

            IsPaused = false;
            IsUnpaused = false;
            isPlayerDead = false;
        }

        private void UnpauseCheckTimer_Tick(object sender, EventArgs e)
        {
            HandlePauseState();
        }
        private void HandlePauseState()
        {
            if (IsUnpaused)
            {
                IsUnpaused = false;
                IsPaused = false;
                Speed = (float)player.Speed;
                endGameScreen = new GameEndControl();
                AttackTimer.Interval = TimeSpan.FromSeconds(1 / player.AttackSpeed);
                GameScreen.GameSpace.Children.Remove(pauseAndShopMenu);
                GameScreen.GameSpace.Focus();
                ResumeGame();
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LeftMouseButtonPressed = true;
            }
        }
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                LeftMouseButtonPressed = false;
            }
        }
        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = true;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = true;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = true;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = true;
            }
            if (e.Key == Key.Space)
            {
                movementDirection = new Vector(SpeedX, SpeedY);
                movementDirection.Normalize();
                Ultimate();
            }
            if (e.Key == Key.Escape)
            {
                if (!IsPaused)
                {
                    IsPaused = true;
                    StopGame();
                    pauseAndShopMenu = new PauseAndShopMenuControl();
                    GameScreen.GameSpace.Children.Add(pauseAndShopMenu);
                    pauseAndShopMenu.Focus();
                }
            }
        }
        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyPressed = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = false;
            }
            if (e.Key == Key.D)
            {
                RightKeyPressed = false;
            }
        }

        private void RegenerationTimer_Tick(object sender, EventArgs e)
        {
            player.CurrentHealth += mainWindow.gameState.CurrentDifficultyMultiplier * 0.5;
            if (player.CurrentHealth > player.MaxHealth)
            {
                player.CurrentHealth = player.MaxHealth;
            }
        }
        private void AttackTimer_Tick(object sender, EventArgs e)
        {
            AttackTimer.Stop();
            attackAvalible = true;
        }
        private void Attack()
        {
            if (attackAvalible)
            {
                attackAvalible = false;
                AttackTimer.Start();

                var characterPosition = playerControl.TranslatePoint(new Point(playerControl.ActualWidth / 2.0, playerControl.ActualHeight / 2.0), GameScreen.GameSpace);
                var position = new Point(characterPosition.X, characterPosition.Y - playerControl.ActualHeight / 2.0);
                Bullet bullet = CreateProjectile(position, facingDirection);
                GameScreen.GameSpace.Children.Add(bullet.UserControl);
                bullet.UserControl.Visibility = Visibility.Visible;
                player.Bullets.Add(bullet);

                DispatcherTimer projectileTimer = new DispatcherTimer();
                projectileTimer.Interval = TimeSpan.FromMilliseconds(16);
                projectileTimer.Tick += (sender, args) =>
                {
                    bullet.Position = new Point(bullet.Position.X + bullet.Direction.X * bullet.Speed, bullet.Position.Y + bullet.Direction.Y * bullet.Speed);
                    bullet.UserControl.SetValue(Canvas.LeftProperty, bullet.Position.X);
                    bullet.UserControl.SetValue(Canvas.TopProperty, bullet.Position.Y);

                    bullet.LifeTime -= 10;
                    if (bullet.LifeTime <= 0)
                    {
                        projectileTimer.Stop();
                        GameScreen.GameSpace.Children.Remove(bullet.UserControl);
                        player.Bullets.Remove(bullet);
                    }
                };
                projectileTimer.Start();
            }
        }
        private Bullet CreateProjectile(Point position, Vector direction)
        {
            sound.PlaySound("player-attack");
            return new Bullet(position, direction, 20, 400, player.Damage, new BulletControl());
        }
        private void UpdateFacingDirection()
        {
            var characterPosition = playerControl.TranslatePoint(new Point(playerControl.ActualWidth / 2.0, playerControl.ActualHeight / 2.0), GameScreen.GameSpace);
            var direction = mousePosition - characterPosition;
            direction.Normalize();
            facingDirection = direction;
        }
        private void GameTick(object sender, EventArgs e)
        {
            ProcessInput();
            UpdateMovement();
            ClampPlayerPosition();
            CheckPlayerDeath();
            ApplyFrictionAndMove();
            UpdateRotationAndFacing();
        }

        private void ProcessInput()
        {
            if (UpKeyPressed)
                SpeedY += Speed;
            if (DownKeyPressed)
                SpeedY -= Speed;
            if (LeftKeyPressed)
                SpeedX -= Speed;
            if (RightKeyPressed)
                SpeedX += Speed;
            if (LeftMouseButtonPressed)
                Attack();
        }

        private void UpdateMovement()
        {
            player.Position = new Vector(translateTransform.X + playerControl.ActualWidth / 2,
                                          translateTransform.Y + playerControl.ActualHeight / 2);
        }

        private void ClampPlayerPosition()
        {
            var maxX = GameScreen.GameSpace.ActualWidth;
            var maxY = GameScreen.GameSpace.ActualHeight;
            if (translateTransform.X < 0)
                translateTransform.X = 0;
            if (translateTransform.X + playerControl.ActualWidth > maxX)
                translateTransform.X = maxX - playerControl.ActualWidth;
            if (translateTransform.Y < 0)
                translateTransform.Y = 0;
            if (translateTransform.Y + playerControl.ActualHeight > maxY)
                translateTransform.Y = maxY - playerControl.ActualHeight;
        }

        private void CheckPlayerDeath()
        {
            IsPlayerDead();
            if (isPlayerDead)
            {
                StopGame();
                isPlayerDead = false;
                endGameScreen.GameEndScore = mainWindow.gameState.CurrentScore;
                endGameScreen.GameEndTime = mainWindow.gameState.CurrentTime;
                GameScreen.GameSpace.Children.Add(endGameScreen);
                endGameScreen.Focus();
                endGameScreen.Update();
            }
        }

        private void ApplyFrictionAndMove()
        {
            SpeedX *= Friction;
            SpeedY *= Friction;
            translateTransform.X += SpeedX;
            translateTransform.Y -= SpeedY;
        }

        private void UpdateRotationAndFacing()
        {
            mousePosition = Mouse.GetPosition(GameScreen);
            RotateCharacterToMouse();
            UpdateFacingDirection();
        }

        private void RotateCharacterToMouse()
        {
            var characterPosition = playerControl.TranslatePoint(new Point(playerControl.ActualWidth / 2.0, playerControl.ActualHeight / 2.0), GameScreen.GameSpace);
            var direction = mousePosition - characterPosition;
            var angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;

            rotateTransform.CenterX = playerControl.ActualWidth / 2.0;
            rotateTransform.CenterY = playerControl.ActualHeight / 2.0;

            rotateTransform.Angle = angle;
        }
        private void GameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.GetPosition(GameScreen.GameSpace);
        }
        private void UltimateTimer_Tick(object sender, EventArgs e)
        {
            UltimateTimer.Stop();
            ultimateAvailable = true;
        }
        private void Ultimate()
        {
            if (ultimateAvailable && player.UltimateID == 1)
            {
                SpeedY = (float)(40.0f * movementDirection.Y);
                SpeedX = (float)(40.0f * movementDirection.X);
                ultimateAvailable = false;
                UltimateTimer.Start();
            }
            if (ultimateAvailable && player.UltimateID == 2)
            {
                ultimateAvailable = false;
                AttackTimer.Interval = TimeSpan.FromSeconds(1.0 / (5 * player.AttackSpeed));
                Ultimate2DurationTimer.Interval = TimeSpan.FromSeconds(3);
                Ultimate2DurationTimer.Tick += (sender, e) =>
                {
                    AttackTimer.Interval = TimeSpan.FromSeconds(1.0 / player.AttackSpeed);
                    Ultimate2DurationTimer.Stop();
                    UltimateTimer.Start();
                };
                Ultimate2DurationTimer.Start();
            }
        }
        private void IsPlayerDead()
        {
            if(player.CurrentHealth <= 0)
            {
                isPlayerDead = true;
            }
        }

        private void StopGame()
        {
            GameTimer.Stop();
            UltimateTimer.Stop();
            AttackTimer.Stop();
            attackAvalible = false;
            RegenerationTimer.Stop();
			mainWindow.enemyControls.StopUpdate();
			mainWindow.gameState.IsPaused = true;
        }
        private void ResumeGame()
        {
            GameTimer.Start();
            UltimateTimer.Start();
            AttackTimer.Start();
            attackAvalible = true;
            RegenerationTimer.Start();
			mainWindow.enemyControls.ResumeUpdate();
			mainWindow.gameState.IsPaused = false;
        }
    }
}
