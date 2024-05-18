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
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Project.Assets.ControlClasses
{
    public class GameControls
    {
        public Canvas GameScreen { get; set; }
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed, LeftMouseButtonPressed;
        private float SpeedX, SpeedY, Friction = 0.75f, Speed;
        private Point mousePosition;

        public UserControl character1Control { get; set;}
        private static Player character1;

        private TranslateTransform translateTransform;
        private RotateTransform rotateTransform;
        private TransformGroup combinedTransform;

        private Vector movementDirection;
        private bool jumpAvailable = true, attackAvalible = true;
        private DispatcherTimer JumpTimer = new DispatcherTimer();

        private DispatcherTimer AttackTimer = new DispatcherTimer();
        private Vector facingDirection;

        public GameControls(Canvas gameScreen, Player player)
        {
            character1 = player;
            Speed = (float)character1.Speed;
            GameScreen = gameScreen;

            character1Control = new Character1Control(character1);
            GameScreen.Children.Add(character1Control);

            translateTransform = new TranslateTransform();
            rotateTransform = new RotateTransform();
            combinedTransform = new TransformGroup();
            combinedTransform.Children.Add(rotateTransform);
            combinedTransform.Children.Add(translateTransform);
            character1Control.RenderTransform = combinedTransform;

            movementDirection = new Vector(1, 0);

            facingDirection = new Vector(1, 0);
        }
        public void StartGame()
        {
            translateTransform.X = character1.Position.X;
            translateTransform.Y = character1.Position.Y;

            GameScreen.KeyDown += KeyboardDown;
            GameScreen.KeyUp += KeyboardUp;
            GameScreen.MouseMove += GameScreen_MouseMove;

            GameScreen.MouseDown += MouseDown;
            GameScreen.MouseUp += MouseUp;

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();

            JumpTimer.Interval = TimeSpan.FromSeconds(3);
            JumpTimer.Tick += JumpTimer_Tick;

            AttackTimer.Interval = TimeSpan.FromSeconds(character1.AttackSpeed);
            AttackTimer.Tick += AttackTimer_Tick;
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LeftMouseButtonPressed = true;
                Attack();
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
                Jump();
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

                var characterPosition = character1Control.TranslatePoint(new Point(character1Control.ActualWidth / 2.0, character1Control.ActualHeight / 2.0), GameScreen);
                var position = new Point(characterPosition.X, characterPosition.Y - character1Control.ActualHeight / 2.0);
                Bullet bullet = CreateProjectile(position, facingDirection);
                GameScreen.Children.Add(bullet.UserControl);
                bullet.UserControl.Visibility = Visibility.Visible;
                character1.Bullets.Add(bullet);

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
                        GameScreen.Children.Remove(bullet.UserControl);
                        character1.Bullets.Remove(bullet);
                    }
                };
                projectileTimer.Start();
            }
        }
        private Bullet CreateProjectile(Point position, Vector direction)
        {
            return new Bullet(position, direction, 20, 400, character1.Damage, new BulletControl());
        }
        private void UpdateFacingDirection()
        {
            var characterPosition = character1Control.TranslatePoint(new Point(character1Control.ActualWidth / 2.0, character1Control.ActualHeight / 2.0), GameScreen);
            var direction = mousePosition - characterPosition;
            direction.Normalize();
            facingDirection = direction;
        }
        private void GameTick(object sender, EventArgs e)
        {
            if (UpKeyPressed)
            {
                SpeedY += Speed;
            }
            if (DownKeyPressed)
            {
                SpeedY -= Speed;
            }
            if (LeftKeyPressed)
            {
                SpeedX -= Speed;
            }
            if (RightKeyPressed)
            {
                SpeedX += Speed;
            }

            var maxX = GameScreen.ActualWidth;
            var maxY = GameScreen.ActualHeight;
            character1.Position = new Vector(translateTransform.X + character1Control.ActualWidth / 2, translateTransform.Y + character1Control.ActualHeight / 2);

            if (translateTransform.X < 0)
            {
                translateTransform.X = 0;
            }
            if (translateTransform.X + character1Control.ActualWidth > maxX)
            {
                translateTransform.X = maxX - character1Control.ActualWidth;
            }
            if (translateTransform.Y < 0)
            {
                translateTransform.Y = 0;
            }
            if (translateTransform.Y + character1Control.ActualHeight > maxY)
            {
                translateTransform.Y = maxY - character1Control.ActualHeight;
            }


            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;

            translateTransform.X += SpeedX;
            translateTransform.Y -= SpeedY;

            mousePosition = Mouse.GetPosition(GameScreen);
            RotateCharacterToMouse();

            UpdateFacingDirection();
        }
        private void RotateCharacterToMouse()
        {
            var characterPosition = character1Control.TranslatePoint(new Point(character1Control.ActualWidth / 2.0, character1Control.ActualHeight / 2.0), GameScreen);
            var direction = mousePosition - characterPosition;
            var angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;

            rotateTransform.CenterX = character1Control.ActualWidth / 2.0;
            rotateTransform.CenterY = character1Control.ActualHeight / 2.0;

            rotateTransform.Angle = angle;
        }
        private void GameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.GetPosition(GameScreen);
        }
        private void JumpTimer_Tick(object sender, EventArgs e)
        {
            JumpTimer.Stop();
            jumpAvailable = true;
        }
        private void Jump()
        {
            if (jumpAvailable)
            {
                SpeedY = (float)(character1.JumpLenght * movementDirection.Y);
                SpeedX = (float)(character1.JumpLenght * movementDirection.X);
                jumpAvailable = false;
                JumpTimer.Start();
            }
        }
    }
}
